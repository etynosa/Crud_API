namespace Crud_API.DomainModels.Base
{
    public class ErrorResult<TError> : IResult<TError>
    {
        public ErrorResult()
        {
            Errors = new List<TError>();
        }

        public ErrorResult(TError error)
        {
            Errors = new List<TError>();
            Errors.Add(error);
        }

        public ErrorResult(IResult<TError> generalErrorResult)
        {
            Errors = generalErrorResult.Errors;
        }

        public ErrorResult(IEnumerable<TError> errors)
        {
            Errors = new List<TError>(errors);
        }

        public bool Success => false;

        public IList<TError> Errors { get; protected set; }

        public void AddError(TError error)
        {
            Errors.Add(error);
        }
    }

    public class ErrorResult<TData, TError> : ErrorResult<TError>, IResult<TData, TError>
    {
        public ErrorResult()
        {
        }

        public ErrorResult(TError error)
        {
            Errors = new List<TError>();
            Errors.Add(error);
        }

        public ErrorResult(IResult<TError> generalErrorResult)
        {
            Errors = generalErrorResult.Errors;
        }

        public ErrorResult(IEnumerable<TError> errors)
        {
            Errors = new List<TError>(errors);
        }

        public TData Data { get; set; }
    }
}
