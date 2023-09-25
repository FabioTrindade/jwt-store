using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using JwtStore.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Authenticate;

public class Repository : IRepository
{
    private readonly AppDbContext _context;
    public Repository(AppDbContext context)
        => _context = context;

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    => await _context
        .User
        .AsNoTracking()
        .FirstOrDefaultAsync(f => f.Email.Address == email, cancellationToken);
}