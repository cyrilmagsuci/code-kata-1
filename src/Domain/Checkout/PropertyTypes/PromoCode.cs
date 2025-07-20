using Domain.Checkout.Errors;
using SharedKernel;

namespace Domain.Checkout.PropertyTypes;

public sealed record PromoCode: PropertyType<PromoCode, string>
{
    protected override Result<string> Validate()
    {
        return string.IsNullOrEmpty(Value) ? Result.Failure<string>(PromoCodeErrors.Empty) : Result.Success(Value);
    }
}
