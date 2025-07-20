using Domain.Checkout.Errors;
using SharedKernel;

namespace Domain.Checkout.PropertyTypes;

public sealed record Sku : PropertyType<Sku, string>
{
    protected override Result<string> Validate()
    {
        return string.IsNullOrEmpty(Value) ? Result.Failure<string>(SkuErrors.Empty) : Result.Success(Value);
    }
}
