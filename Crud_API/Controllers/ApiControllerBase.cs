using Crud_API.DomainModels.Base;
using Crud_API.DomainModels.Constants;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Crud_API.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected readonly ILogger<ApiControllerBase> _logger;
        public ApiControllerBase(ILogger<ApiControllerBase> logger)
        {
            _logger = logger;
        }

        protected IActionResult HandleResult<TData, TError>(IResult<TData, TError> result)
        {
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return HandleErrors(result);
            }
        }

        protected IActionResult HandleResult<TError>(IResult<TError> result)
        {
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return HandleErrors(result);
            }
        }

        private IActionResult HandleErrors<TError>(IResult<TError> result)
        {
            var errors = new List<string>();
            errors.AddRange(result.Errors.Select(x => GetError((Enum)(object)x)));

            if (errors.Count == 0)
            {
                errors.Add(ErrorMessageConstants.DefaultErrorMessage);
            }

            errors.ForEach(x => _logger.LogError(x));
            return BadRequest(errors);
        }

        private string GetError(Enum error)
        {
            var errorMessage = string.Empty;

            if (!ErrorMessageConstants.ErrorMessages.TryGetValue(error, out errorMessage))
            {
                errorMessage = ErrorMessageConstants.DefaultErrorMessage;
            }

            return errorMessage;
        }
    }
}
