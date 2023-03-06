namespace Puzzle.Lib.Auth.Infrastructure.Constants
{
    internal static class ExceptionMessage
    {
        internal const string ClaimTypeEmailRequired = "Email request type not found.";
        internal const string ClaimTypeNameRequired = "Name claim type not found.";
        internal const string ClaimTypeNameIdentifierRequired = "Name identifier (id) claim type not found.";
        internal const string ClaimTypeRoleRequired = "The role claim type could not be found.";
        internal const string ClaimTypeJtiRequired = "Jti unique key claim type not found.";
        internal const string ClaimTypeAudienceRequired = "The provider claim type could not be found.";
        internal const string ClaimTypeUsernameRequired = "Username claim type not found.";
    }
}
