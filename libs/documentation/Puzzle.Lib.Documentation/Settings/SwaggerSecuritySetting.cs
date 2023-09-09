namespace Puzzle.Lib.Documentation.Settings;

/// <summary>
/// Represents the configuration settings for Swagger security.
/// </summary>
public sealed record SwaggerSecuritySetting
{
    /// <summary>
    /// Gets or initializes the username required for authentication.
    /// </summary>
    [JsonRequired]
    public required string Username { get; init; }

    /// <summary>
    /// Gets or initializes the password required for authentication.
    /// </summary>
    [JsonRequired]
    public required string Password { get; init; }
}
