namespace Puzzle.Lib.Documentation.Settings;

/// <summary>
/// Represents the configuration settings for Swagger documentation.
/// </summary>
public sealed record SwaggerSetting
{
    /// <summary>
    /// Gets or setializes the title of the Swagger documentation.
    /// </summary>
    [JsonRequired]
    public string Title { get; set; }

    /// <summary>
    /// Gets or setializes the description of the Swagger documentation.
    /// </summary>
    [JsonRequired]
    public string Description { get; set; }

    /// <summary>
    /// Gets or setializes the version of the Swagger documentation.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Gets or setializes the name of the contact for the Swagger documentation.
    /// </summary>
    public string ContactName { get; set; }

    /// <summary>
    /// Gets or setializes the email address of the contact for the Swagger documentation.
    /// </summary>
    public string ContactEmail { get; set; }
}