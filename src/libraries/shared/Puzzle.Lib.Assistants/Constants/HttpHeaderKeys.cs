namespace Puzzle.Lib.Assistants.Constants
{
    public record struct HttpHeaderKeys
    {
        public const string Authentication = "Authentication";
        public const string Authorization = "Authorization";
        public const string Bearer = "Bearer";

        public const string PlatformType = "x-platform-type";
       
        public const string CorrelationId = "x-correlation-id";
        public const string ApiVersion = "x-api-version";
        public const string ForwardedFor = "X-forwarded-for";
        public const string FolderName = "x-folder-name";

        public const string RequestedWithHeader = "x-requested-with";
        public const string XmlHttpRequest = "XMLHttpRequest";
    }
}
