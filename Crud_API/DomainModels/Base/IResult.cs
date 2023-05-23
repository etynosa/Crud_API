namespace Crud_API.DomainModels.Base
{
    public interface IResult<TError>
    {
        bool Success { get; }

        IList<TError> Errors { get; }

        void AddError(TError error);
    }

    public interface IResult<TData, TError> : IResult<TError>
    {
        TData Data { get; set; }
    }
}
