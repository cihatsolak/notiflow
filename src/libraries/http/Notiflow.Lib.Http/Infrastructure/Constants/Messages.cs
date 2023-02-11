namespace Notiflow.Lib.Http.Infrastructure.Constants
{
    internal static class ExceptionMessage
    {
        internal const string TokenNotFound = "No token found to add to request header.";
        internal const string NameNotFound = "Could not find namespace to add to request header.";
        internal const string ValueNotFound = "Value field not found to add to request header.";
        internal const string ClientNameRequired = "Client name is required.";
    }
}
