using Domain.Checkout;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class CheckoutItemConfiguration : IEntityTypeConfiguration<CheckoutItem>
{
    public void Configure(EntityTypeBuilder<CheckoutItem> builder)
    {
        builder.HasKey(u => u.Id);

        builder.ComplexProperty(
            u => u.Quantity,
            b => b.Property(e => e.Value).HasColumnName("quantity"));

        builder.ComplexProperty(
            u => u.Sku,
            b => b.Property(e => e.Value).HasColumnName("sku"));
        
        builder.ComplexProperty(
            u => u.UnitOfMeasure,
            b => b.Property(e => e.Value).HasColumnName("uom"));

        builder.HasOne<Checkout>()
            .WithMany(x => x.CheckoutItems)
            .HasForeignKey(x => x.CheckoutId);
    }
}
