using Crud_API.DomainModels.Enums;

namespace Crud_API.DomainModels.Constants
{
    public static class ErrorMessageConstants
    {
        public static string DefaultErrorMessage => "An error occured.";

        public static Dictionary<Enum, string> ErrorMessages => new Dictionary<Enum, string>()
        {
            [GenericValidationEnum.EntityNotFound] = "Entity not found.",
            [GenericValidationEnum.DuplicatedEntityFound] = "Duplicated entity found.",
            [GenericValidationEnum.InvalidRequest] = "Invalid request.",
            [GenericValidationEnum.PermissionsAreRequired] = "Permissions are required."
        };
    }
}
