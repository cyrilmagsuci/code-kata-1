namespace SharedKernel;

public abstract record PropertyType<TProp, TValue>
    where TProp : PropertyType<TProp, TValue>, new()
{
    public static Result<TProp> Empty => From(default!);

    public static Result<TProp> Create(TValue value)
    {
        var prop = new TProp
        {
            Value = value
        };
        Result<TValue> validationResult = prop.Validate();
        if (validationResult.IsFailure)
        {
            return Result.Failure<TProp>(validationResult.Error);
        }
        return prop;
    }
    
    public static TProp From(TValue value)
    {
        Result<TProp> propResult = Create(value);
        if (propResult.IsFailure)
        {
            throw new PropertyTypeValidationException(propResult.Error);
        }
       
        return propResult.Value;
    }
    
    public TValue Value { get; private set; }

    protected virtual Result<TValue> Validate()
    {
        return Result.Success(Value);
    }
}
