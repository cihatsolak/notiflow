namespace Puzzle.Lib.Documentation.Settings;

/// <summary>
/// Represents the configuration settings for Swagger documentation.
/// </summary>
public sealed record SwaggerSetting
{
    /// <summary>
    /// Gets or sets the title of the Swagger documentation.
    /// </summary>
    [JsonRequired]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the Swagger documentation.
    /// </summary>
    [JsonRequired]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the version of the Swagger documentation.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Gets or sets the name of the contact for the Swagger documentation.
    /// </summary>
    public string ContactName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the contact for the Swagger documentation.
    /// </summary>
    public string ContactEmail { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the application is running in a production environment.
    /// </summary>
    public bool IsProductionEnvironment { get; set; }
}
