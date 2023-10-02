namespace Puzzle.Lib.Security.Infrastructure.Settings;

/// <summary>
/// Represents the CORS settings for Angular, Blazor, and React applications.
/// </summary>
/// <remarks>
/// This is a public sealed record that contains three properties, Angular, Blazor, and React,
/// which are strings representing the CORS settings for each application.
/// </remarks>
public sealed record CorsSetting
{
    /// <summary>
    /// Gets or sets the CORS settings for spa applications.
    /// </summary>
    public string[] Origins { get; set; }
}
