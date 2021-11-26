namespace ExampleProjectsCommomResources.Domain.Models.ApplicationModels
{
    public struct Result
    {
        public bool OperationFailed { get; }
        public Error Error { get; }

        private Result(bool operationFailed, Error error)
        {
            OperationFailed = operationFailed;
            Error = error;
        }

        public static Result Success() => new Result(false, null);
        public static Result Fail(Error error) => new Result(true, error);
    }

    public struct Result<T>
    {
        public T SuccessData { get; }
        public bool OperationFailed { get; }
        public Error Error { get; }

        private Result(bool operationFailed, T successData)
        {
            OperationFailed = operationFailed;
            Error = null;
            SuccessData = successData;
        }

        private Result(bool operationFailed, Error error)
        {
            OperationFailed = operationFailed;
            Error = error;
            SuccessData = default(T);
        }

        public void Deconstruct(out bool operationFailed, out Error error, out T successData)
        {
            operationFailed = OperationFailed;
            error = Error;
            successData = SuccessData;
        }

        public static implicit operator Result<T>(Result result) =>
            result.OperationFailed
            ? new Result<T>(true, result.Error)
            : new Result<T>(false, default(T));

        public static Result<T> Success(T successData) => new Result<T>(false, successData);
        public static Result<T> Fail(Error error) => new Result<T>(true, error);
    }
}
