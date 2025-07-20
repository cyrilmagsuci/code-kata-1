using Domain.Checkout;
using Domain.Checkout.PropertyTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel;

namespace Infrastructure.Database.Configurations;

internal sealed class CheckoutConfiguration : IEntityTypeConfiguration<Checkout>
{
    public void Configure(EntityTypeBuilder<Checkout> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.PromoCodes)
            .HasConversion(
                v => JsonHelper.Serialize(v),
                v => JsonHelper.Deserialize<List<PromoCode>>(v))
            .HasColumnName("promo_codes");
    }
}
