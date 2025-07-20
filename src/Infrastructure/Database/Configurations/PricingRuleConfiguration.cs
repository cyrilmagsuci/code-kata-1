using Domain.Checkout;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class PricingRuleConfiguration : IEntityTypeConfiguration<PricingRule>
{
    public void Configure(EntityTypeBuilder<PricingRule> builder)
    {
        builder.HasKey(u => u.Id);

        builder.ComplexProperty(
            u => u.Quantity,
            b => b.Property(e => e.Value).HasColumnName("quantity"));

        builder.ComplexProperty(
            u => u.Sku,
            b => b.Property(e => e.Value).HasColumnName("sku"));

        builder.ComplexProperty(
            u => u.PromoCode,
            b => b.Property(e => e.Value).HasColumnName("promo_code"));

        builder.ComplexProperty(
            u => u.PricePerUnit,
            b => b.Property(e => e.Value).HasColumnName("price_per_unit"));

        builder.ComplexProperty(
            u => u.UnitOfMeasure,
            b => b.Property(e => e.Value).HasColumnName("uom"));
    }
}
