using Domain.Checkout;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Domain.Checkout.Checkout> Checkouts { get; set; }
    DbSet<CheckoutItem> CheckoutItems { get; set; }
    DbSet<Domain.Checkout.PricingRule> PricingRules { get; set; }
    void AddEntity<TEntity>(TEntity entity)
        where TEntity : Entity;
    void UpdateEntity<TEntity>(TEntity entity)
        where TEntity : Entity;
    void RemoveEntity<TEntity>(TEntity entity)
        where TEntity : Entity;
}
