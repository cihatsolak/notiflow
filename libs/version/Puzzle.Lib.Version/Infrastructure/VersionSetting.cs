namespace Puzzle.Lib.Version.Infrastructure;

/// <summary>
/// Represents the API version settings.
/// </summary>
public sealed record ApiVersionSetting
{
    /// <summary>
    /// Gets or sets the major version number of the API.
    /// </summary>
    public int MajorVersion { get; set; }

    /// <summary>
    /// Gets or sets the minor version number of the API.
    /// </summary>
    public int MinorVersion { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to enable versioned API exploration.
    /// </summary>
    public bool EnableVersionedApiExplorer { get; set; }
}
