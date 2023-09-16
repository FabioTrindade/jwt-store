using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Infra.Contexts.AccountContext.Mappings;
using Microsoft.EntityFrameworkCore;

namespace JwtStore.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base (options)
    {
    }

    public DbSet<User> User { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}