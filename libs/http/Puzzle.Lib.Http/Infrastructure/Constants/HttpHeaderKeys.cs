namespace Puzzle.Lib.Http.Infrastructure.Constants;

/// <summary>
/// A static class containing constant values for commonly used HTTP header keys.
/// </summary>
public record struct HttpHeaderKeys
{
    /// <summary>
    /// The key for the Authentication HTTP header.
    /// </summary>
    public const string Authentication = "Authentication";

    /// <summary>
    /// The key for the Authorization HTTP header.
    /// </summary>
    public const string Authorization = "Authorization";

    /// <summary>
    /// The value for the Bearer authentication scheme.
    /// </summary>
    public const string Bearer = "Bearer";

    /// <summary>
    /// The key for the x-platform-type HTTP header.
    /// </summary>
    public const string PlatformType = "x-platform-type";

    /// <summary>
    /// The key for the x-correlation-id HTTP header.
    /// </summary>
    public const string CorrelationId = "x-correlation-id";

    /// <summary>
    /// The key for the x-api-version HTTP header.
    /// </summary>
    public const string ApiVersion = "x-api-version";

    /// <summary>
    /// The key for the X-forwarded-for HTTP header.
    /// </summary>
    public const string ForwardedFor = "X-forwarded-for";

    /// <summary>
    /// The key for the x-folder-name HTTP header.
    /// </summary>
    public const string FolderName = "x-folder-name";

    /// <summary>
    /// The key for the x-requested-with HTTP header.
    /// </summary>
    public const string RequestedWithHeader = "x-requested-with";

    /// <summary>
    /// The value for the XmlHttpRequest header.
    /// </summary>
    public const string XmlHttpRequest = "XMLHttpRequest";
}
