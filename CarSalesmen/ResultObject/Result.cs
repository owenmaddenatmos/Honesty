namespace CarSalesmen.ResultObject
{
    public class Result
    {
        public static Result<TError> Ok<TError>()
        {
            return new Result<TError>(true, default(TError));
        }

        public static Result<TError, TValue> Ok<TError, TValue>(TValue value)
        {
            return new Result<TError, TValue>(value, true, default(TError));
        }

        public static Result<TError> Fail<TError>(TError error)
        {
            return new Result<TError>(false, error);
        }

        public static Result<TError, TValue> Fail<TError, TValue>(TError error)
        {
            return new Result<TError, TValue>(default(TValue), false, error);
        }
    }
}
