using Domain.Checkout.Errors;
using SharedKernel;

namespace Domain.Checkout.PropertyTypes;

public sealed record UnitOfMeasure : PropertyType<UnitOfMeasure, string>
{
    protected override Result<string> Validate()
    {
        return string.IsNullOrEmpty(Value) ? Result.Failure<string>(UnitOfMeasureErrors.Empty) : Result.Success(Value);
    }
}
