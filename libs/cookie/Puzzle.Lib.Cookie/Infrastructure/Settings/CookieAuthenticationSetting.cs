namespace Puzzle.Lib.Cookie.Infrastructure.Settings;

/// <summary>
/// Represents a collection of CookieAuthentication settings.
/// </summary>
public sealed record CookieAuthenticationSetting
{
    /// <summary>
    /// Gets or sets the path to the page where users can log in.
    /// </summary>
    [JsonRequired]
    public required string LoginPath { get; init; }

    /// <summary>
    /// Gets or sets the path to the page where users can log out.
    /// </summary>
    public required string LogoutPath { get; init; }

    /// <summary>
    /// Gets or sets the path to the page where users are redirected if their access is denied.
    /// </summary>
    public required string AccessDeniedPath { get; init; }

    /// <summary>
    /// Gets or sets the expiration time of the session in hours.
    /// </summary>
    public required int ExpireHour { get; init; }
}
