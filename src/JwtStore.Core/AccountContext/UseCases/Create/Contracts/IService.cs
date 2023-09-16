using JwtStore.Core.AccountContext.Entities;

namespace JwtStore.Core.AccountContext.UseCases.Create.Contract;

public interface IService
{
    Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
}