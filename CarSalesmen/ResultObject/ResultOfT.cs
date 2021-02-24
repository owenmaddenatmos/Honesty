using System;

namespace CarSalesmen.ResultObject
{
    public class Result<TError>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public TError Error { get; }

        protected internal Result(bool isSuccess, TError error)
        {
            if (!isSuccess && error == null)
                throw new InvalidOperationException("Unsuccessful result was supplied without an error");

            IsSuccess = isSuccess;
            Error = error;
        }
    }
}