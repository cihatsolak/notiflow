namespace Puzzle.Lib.Security.Infrastructure;

/// <summary>
/// Represents the settings for encryption, including the encryption key.
/// </summary>
public sealed record AesCipherSetting
{
    /// <summary>
    /// Gets or initializes the encryption key.
    /// </summary>
    [JsonRequired]
    public string RgbKey { get; set; }
}

