namespace Puzzle.Lib.Auth.Models;

/// <summary>
/// Represents a response containing access and refresh tokens along with their expiration information.
/// </summary>
public sealed record TokenResponse
{
    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    [JsonRequired]
    public required string AccessToken { get; init; }

    /// <summary>
    /// Gets or sets the date and time when the access token expires.
    /// </summary>
    public required DateTime AccessTokenExpiration { get; init; }

    /// <summary>
    /// Gets or sets the number of seconds until the access token expires.
    /// </summary>
    public required int ExpiresIn { get; init; }

    /// <summary>
    /// Gets or sets the refresh token.
    /// </summary>
    public required string RefreshToken { get; init; }

    /// <summary>
    /// Gets or sets the date and time when the refresh token expires.
    /// </summary>
    public required DateTime RefreshTokenExpiration { get; init; }
}
