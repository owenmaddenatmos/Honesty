using System;

namespace CarSalesmen.ResultObject
{
    public class Result<TError, T> : Result<TError>
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("Attempt to access the value of an unsuccessful result");

                return _value;
            }
        }

        protected internal Result(T value, bool isSuccess, TError error) : base(isSuccess, error)
        {
            //Have to remove these if we are to support TError that are not nullable (e.g. int)
            if (isSuccess && value == null)
                throw new InvalidOperationException("Attempt to create result with successful result and supplied null value");
            _value = value;
        }
    }
}