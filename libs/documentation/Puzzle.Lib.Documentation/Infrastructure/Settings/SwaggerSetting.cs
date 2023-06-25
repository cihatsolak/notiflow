namespace Puzzle.Lib.Documentation.Infrastructure.Settings;

/// <summary>
/// Represents the configuration settings for Swagger documentation.
/// </summary>
public sealed record SwaggerSetting
{
    /// <summary>
    /// Gets or initializes the title of the Swagger documentation.
    /// </summary>
    [JsonRequired]
    public required string Title { get; init; }

    /// <summary>
    /// Gets or initializes the description of the Swagger documentation.
    /// </summary>
    [JsonRequired]
    public required string Description { get; init; }

    /// <summary>
    /// Gets or initializes the version of the Swagger documentation.
    /// </summary>
    public required string Version { get; init; }

    /// <summary>
    /// Gets or initializes the name of the contact for the Swagger documentation.
    /// </summary>
    public string ContactName { get; init; }

    /// <summary>
    /// Gets or initializes the email address of the contact for the Swagger documentation.
    /// </summary>
    public string ContactEmail { get; init; }
}