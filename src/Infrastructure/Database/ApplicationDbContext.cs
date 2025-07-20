using System.Data;
using Application.Abstractions.Data;
using Domain.Checkout;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SharedKernel;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IUnitOfWork, IApplicationDbContext
{
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<CheckoutItem> CheckoutItems { get; set; }
    public DbSet<PricingRule> PricingRules { get; set; }
    public void AddEntity<TEntity>(TEntity entity) where TEntity : Entity
    {
        Set<TEntity>().Add(entity);
    }

    public void UpdateEntity<TEntity>(TEntity entity) where TEntity : Entity
    {
        Set<TEntity>().Update(entity);
    }

    public void RemoveEntity<TEntity>(TEntity entity) where TEntity : Entity
    {
        Set<TEntity>().Remove(entity);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    public async Task<IDbTransaction> BeginTransactionAsync()
    {
        return (await Database.BeginTransactionAsync()).GetDbTransaction();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
