namespace Puzzle.Lib.Social.Infrastructure;

/// <summary>
/// Represents the social settings for the application.
/// </summary>
public sealed record SocialSettings
{
    /// <summary>
    /// Gets or sets the Google authentication configuration.
    /// </summary>
    public GoogleAuthConfig GoogleAuthConfig { get; set; }

    /// <summary>
    /// Gets or sets the Facebook authentication configuration.
    /// </summary>
    public FacebookAuthConfig FacebookAuthConfig { get; set; }
}

/// <summary>
/// Represents the Google authentication configuration.
/// </summary>
public sealed record GoogleAuthConfig
{
    /// <summary>
    /// Gets or sets the Google client ID.
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the Google client secret.
    /// </summary>
    public string ClientSecret { get; set; }
}

/// <summary>
/// Represents the Facebook authentication configuration.
/// </summary>
public sealed record FacebookAuthConfig
{
    /// <summary>
    /// Gets or sets the base URL for Facebook authentication.
    /// </summary>
    public string BaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the token validation URL for Facebook authentication.
    /// </summary>
    public string TokenValidationUrl { get; set; }

    /// <summary>
    /// Gets or sets the user info URL for Facebook authentication.
    /// </summary>
    public string UserInfoUrl { get; set; }

    /// <summary>
    /// Gets or sets the Facebook app ID.
    /// </summary>
    public string AppId { get; set; }

    /// <summary>
    /// Gets or sets the Facebook app secret.
    /// </summary>
    public string AppSecret { get; set; }
}
