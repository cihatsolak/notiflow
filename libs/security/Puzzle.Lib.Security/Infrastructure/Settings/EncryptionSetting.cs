namespace Puzzle.Lib.Security.Infrastructure.Settings;

/// <summary>
/// Represents the settings for encryption, including the encryption key.
/// </summary>
public sealed record EncryptionSetting
{
    /// <summary>
    /// Gets or initializes the encryption key.
    /// </summary>
    [JsonRequired]
    public string Key { get; set; }
}

