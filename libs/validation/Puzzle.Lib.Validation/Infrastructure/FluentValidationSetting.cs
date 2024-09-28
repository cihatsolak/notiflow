namespace Puzzle.Lib.Validation.Infrastructure;

/// <summary>
/// Represents the settings for FluentValidation.
/// </summary>
public sealed record FluentValidationSetting
{
    /// <summary>
    /// Gets or sets the culture information used for validation error messages.
    /// </summary>
    public CultureInfo CultureInfo { get; set; } = CultureInfo.CurrentCulture;

    /// <summary>
    /// Gets or sets the cascade mode for validation.
    /// </summary>
    public CascadeMode CascadeMode { get; set; } = CascadeMode.Stop;
}
