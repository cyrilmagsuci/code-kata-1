using System.Data;
using Application.Abstractions.Data;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        string promoCode = "default";
        string unitOfMeasure = "piece";
        var pricingRules = new List<PricingRuleInfo>()
        {
            new(
                Guid.NewGuid(), 
                promoCode,
                "A",
                50,
                1,
                unitOfMeasure,
                IsBundle: false),

            new(
                Guid.NewGuid(), 
                promoCode,
                "A",
                130,
                3,
                unitOfMeasure,
                IsBundle: true),

            new(
                Guid.NewGuid(), 
                promoCode,
                "B",
                30,
                1,
                unitOfMeasure,
                IsBundle: false),

            new(
                Guid.NewGuid(), 
                promoCode,
                "B",
                45,
                2,
                unitOfMeasure,
                IsBundle: true),

            new(
                Guid.NewGuid(), 
                promoCode,
                "C",
                20,
                1,
                unitOfMeasure,
                IsBundle: false),

            new(
                Guid.NewGuid(), 
                promoCode,
                "D",
                15,
                1,
                unitOfMeasure,
                IsBundle: false),
        };
        
        const string sql = """
            INSERT INTO public.pricing_rules
            (id, "is_bundle", price_per_unit, promo_code, quantity, sku, uom)
            VALUES(@Id, @IsBundle, @PricePerUnit, @PromoCode, @Quantity, @Sku, @UnitOfMeasure);
            """;

        connection.Execute("DELETE FROM public.pricing_rules");
        connection.Execute(sql, pricingRules);
    }

    private record PricingRuleInfo(
        Guid Id,
        string PromoCode,
        string Sku,
        decimal PricePerUnit,
        decimal Quantity,
        string UnitOfMeasure,
        bool IsBundle);
}
