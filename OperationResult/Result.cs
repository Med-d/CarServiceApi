namespace OperationResult;

public class Result<TResult, TError>
where TError : Exception
{
    private readonly TResult? result;
    private readonly TError? error;

    internal Result(TResult? result, TError? error)
    {
        this.result = result;
        this.error = error;
    }

    internal Result() : this(default, default)
    { }
    
    internal Result(TResult? result) : this(result, default)
    { }

    internal Result(TError? error) : this(default, error)
    { }

    // Static creating operation result for success
    public static Result<TValue, TError> Success<TValue>(TValue result) where TValue : class => new(result);

    // Static creating operation result for failure
    public static Result<TValue, TError> Fail<TValue>(TError error) => new(error);

    // Method for checking if operation result is success
    public bool HasResult() => result!= null;
    
    // Method for checking if operation result is failure
    public bool HasError() => error != null;
    
    // Method for returing operation result if success or default
    public TResult? ValueOrDefault() => HasResult()? result : default;
    
    // Method for returning operation result if success else throw custom exception from error
    public TResult ValueOrThrow()
    {
        if (HasResult())
            return result!;
        throw error!;
    }
}