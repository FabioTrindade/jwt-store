using JwtStore.Core.AccountContext.Entities;

namespace JwtStore.Core.AccountContext.UseCases.Create.Contract;

public interface IRepository
{
    Task<bool> AnyAsync(string email, CancellationToken cancellationToken);

    Task SaveAsync(User user, CancellationToken cancellationToken);
}