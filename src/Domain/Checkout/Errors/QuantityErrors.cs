using SharedKernel;

namespace Domain.Checkout.Errors;

public static class QuantityErrors
{
    public static readonly Error ZeroOrNegative = Error.Problem("Quantity.ZeroOrNegative", "Quantity is zero or negative");
}
