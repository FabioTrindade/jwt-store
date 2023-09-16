using JwtStore.Core.Contexts.AccountContext.Entities;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contract;

public interface IService
{
    Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
}