using JwtStore.Core.AccountContext.Entities;
using JwtStore.Core.AccountContext.UseCases.Create.Contract;
using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.SharedContext.UseCases;
using static JwtStore.Core.AccountContext.UseCases.Create.Response;

namespace JwtStore.Core.AccountContext.UseCases.Create;

public class Handler
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repository
        , IService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Response> Handle(
        Request request
        , CancellationToken cancellationToken)
    {
        #region 01. Validar a requisição
        try
        {
            var response = Specification.Ensure(request);

            if (!response.IsValid)
                return new Response("Requisição inválida", 400, response.Notifications);
        }
        catch (System.Exception)
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }
        #endregion

        #region 02. Gerar os obejtos
        Email email;
        Password password;
        User user;

        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(request.Name, email, password);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion

        #region 03. Verificar se o usuário existe
        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response("Este E-mail já está em uso", 400);
        }
        catch (Exception)
        {
            return new Response("Falha ao verificar E-mail cadastrado", 500);
        }
        #endregion

        #region 04. Persistir os dados
        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response("Falha ao persistir dados", 500);
        }
        #endregion

        #region 05. Enviar e-mail de ativição
        try
        {
            await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch
        {
            //
        }

        #endregion

        return new Response("Conta criada"
        , new ResponseData(user.Id, user.Name, user.Email));
    }
}