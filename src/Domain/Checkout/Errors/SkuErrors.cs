using SharedKernel;

namespace Domain.Checkout.Errors;

public static class SkuErrors
{
    public static readonly Error Empty = Error.Problem("Sku.Empty", "Sku is empty");
}
