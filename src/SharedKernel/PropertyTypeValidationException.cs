namespace SharedKernel;

public class PropertyTypeValidationException(Error error) : Exception(error.ToString())
{
    public Error Error { get; } = error;
}
