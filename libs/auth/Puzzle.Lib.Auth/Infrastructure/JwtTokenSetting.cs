namespace Puzzle.Lib.Auth.Infrastructure;

/// <summary>
/// Represents a record that contains the settings related to JWT token authentication.
/// </summary>
public sealed record JwtTokenSetting
{
    /// <summary>
    /// Gets or sets the list of valid audiences for the JWT token.
    /// </summary>
    public IEnumerable<string> Audiences { get; set; }

    /// <summary>
    /// Gets or sets the issuer of the JWT token.
    /// </summary>
    [JsonRequired]
    public string Issuer { get; set; }

    /// <summary>
    /// Gets or sets the expiration time of the access token in minutes.
    /// </summary>
    public int AccessTokenExpirationMinute { get; set; }

    /// <summary>
    /// Gets or sets the expiration time of the refresh token in minutes.
    /// </summary>
    public int RefreshTokenExpirationMinute { get; set; }

    /// <summary>
    /// Gets or sets the security key for JWT token authentication.
    /// </summary>
    [JsonRequired]
    public string SecurityKey { get; set; }
}