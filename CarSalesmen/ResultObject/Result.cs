namespace CarSalesmen.ResultObject
{
    public class Result
    {
        public static Result<TError> Ok<TError>()
        {
            return new(true, default);
        }

        public static Result<TError, TValue> Ok<TError, TValue>(TValue value)
        {
            return new(value, true, default);
        }

        public static Result<TError> Fail<TError>(TError error)
        {
            return new(false, error);
        }

        public static Result<TError, TValue> Fail<TError, TValue>(TError error)
        {
            return new(default, false, error);
        }
    }
}
