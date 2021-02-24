using System;

namespace CarSalesmen.ResultObject
{
    public static class ResultExtensions
    {
        public static Result<ErrorType> OnSuccess<ErrorType>(this Result<ErrorType> result, Action action)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (action == null) throw new ArgumentNullException(nameof(action));

            if (result.IsSuccess)
                action.Invoke();

            return result;
        }
        public static Result<ErrorType, ValueType> OnSuccess<ErrorType, ValueType>(this Result<ErrorType, ValueType> result, Action<ValueType> action)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (action == null) throw new ArgumentNullException(nameof(action));

            if (result.IsSuccess)
                action.Invoke(result.Value);

            return result;
        }

        public static Result<ErrorType> OnFailure<ErrorType>(this Result<ErrorType> result, Action action)
        {
            return OnFailureInternal<Result<ErrorType>, ErrorType>(result, (e) => action.Invoke());
        }

        public static Result<ErrorType> OnFailure<ErrorType>(this Result<ErrorType> result, Action<ErrorType> action)
        {
            return OnFailureInternal(result, action);
        }

        public static Result<ErrorType, ValueType> OnFailure<ErrorType, ValueType>(this Result<ErrorType, ValueType> result, Action<ErrorType> action)
        {
            return OnFailureInternal(result, action);
        }


        private static ResultType OnFailureInternal<ResultType, ErrorType>(this ResultType result, Action<ErrorType> action) where ResultType : Result<ErrorType>
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (action == null) throw new ArgumentNullException(nameof(action));

            if (!result.IsSuccess)
                action.Invoke(result.Error);

            return result;
        }

        /// <summary>
        /// Maps the value of a successful result to some new return value
        /// </summary>
        /// <param name="result">The result to map</param>
        /// <param name="map">The function to map from the results value to a new value</param>
        /// <returns>A partial reduced result that can either be finished with an OnFailure, or converted into a new Result</returns>
        public static IPartiallyReducedResult<ErrorType, OutType> MapSuccess<ErrorType, ValueType, OutType>(this Result<ErrorType, ValueType> result, Func<ValueType, OutType> map)
        {
            return result.IsSuccess ?
                new PartiallyReducedResult<ErrorType, OutType>((f) => map.Invoke(result.Value), 
                () => Result.Ok<ErrorType, OutType>(map.Invoke(result.Value))) :
                CreateFailedPartial<ErrorType, OutType>(result);
        }
        /// <summary>
        /// Maps the value of a successful result to some new return value
        /// </summary>
        /// <param name="result">The result to map</param>
        /// <param name="map">The function to map from the results value to a new value</param>
        /// <returns>A partial reduced result that can either be finished with an OnFailure, or converted into a new Result</returns>
        public static IPartiallyReducedResult<ErrorType, OutType> MapSuccess<ErrorType, OutType>(this Result<ErrorType> result, Func<OutType> map)
        {
            return result.IsSuccess ?
                new PartiallyReducedResult<ErrorType, OutType>((f) => map.Invoke(),
                () => Result.Ok<ErrorType, OutType>(map.Invoke())) :
                CreateFailedPartial<ErrorType, OutType>(result);

        }
        private static IPartiallyReducedResult<ErrorType, OutType> CreateFailedPartial<ErrorType, OutType>(Result<ErrorType> result)
        {
            return new PartiallyReducedResult<ErrorType, OutType>((f) => f(result.Error),
                () => Result.Fail<ErrorType, OutType>(result.Error));
        }

        private class PartiallyReducedResult<ErrorType, OutType>
            : IPartiallyReducedResult<ErrorType, OutType>
        {
            private readonly Func<Func<ErrorType, OutType>, OutType> m_Finisher;
            private readonly Func<Result<ErrorType, OutType>> m_AsResultConverter;
            public PartiallyReducedResult(
                Func<Func<ErrorType, OutType>, OutType> finisher,
                Func<Result<ErrorType, OutType>> asResultConverter)
            {
                m_Finisher = finisher;
                m_AsResultConverter = asResultConverter;
            }

            OutType IPartiallyReducedResult<ErrorType, OutType>.OnFailure(Func<ErrorType, OutType> errorMapper)
            {
                return m_Finisher.Invoke(errorMapper);
            }
            Result<ErrorType, OutType> IPartiallyReducedResult<ErrorType, OutType>.AsResult()
            {
                return m_AsResultConverter.Invoke();
            }
        }

        /// <summary>
        /// A Result that has been partial dealt with, but still needs the failure case handling to return a value
        /// </summary>
        /// <typeparam name="ErrorType"></typeparam>
        /// <typeparam name="OutType"></typeparam>
        /// Inner class, but public, really didn't want to clutter namespaces with it
        public interface IPartiallyReducedResult<ErrorType, OutType>
        {
            /// <summary>
            /// Finish handling the result by dealing with the error case
            /// </summary>
            /// <param name="errorMapper">The function that will deal with the error case and provide a return value</param>
            OutType OnFailure(Func<ErrorType, OutType> errorMapper);

            /// <summary>
            /// Wrap this back into a Result with the same error as the original, but now with a mapped value
            /// </summary>
            Result<ErrorType, OutType> AsResult();
        }
    }
}
