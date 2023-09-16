using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contract;
using JwtStore.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Create;

public class Repository : IRepository
{
    private readonly AppDbContext _context;
    public Repository(AppDbContext context)
        =>  _context= context;

    public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken)
    => await _context
                .User
                .AsNoTracking()
                .AnyAsync(a => a.Email == email, cancellationToken);

    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        await _context.User.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}