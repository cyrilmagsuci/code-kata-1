using SharedKernel;

namespace Domain.Checkout.Errors;

public static class UnitOfMeasureErrors
{
    public static readonly Error Empty = Error.Problem("UnitOfMeasure.Empty", "UnitOfMeasure is empty");
}
