namespace Puzzle.Lib.Validation.RuleBuilders;

/// <summary>
/// Static class that provides extensions for building rules for FormFiles.
/// </summary>
public static class FormRuleBuilderExtensions
{
    /// <summary>
    /// Represents the value of 1 kilobyte in bytes.
    /// </summary>
    private const int ONE_KB = 1 * 1024;

    /// <summary>
    /// Represents the value of 2 megabytes in bytes.
    /// </summary>
    private const int TWO_MB = 2 * 1024 * 1024;

    /// <summary>
    /// Adds a rule for validating a general FormFile, enforcing size limits and content types.
    /// </summary>
    /// <typeparam name="TElement">The type of the element being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder for the FormFile.</param>
    /// <param name="errorMessage">The error message to be displayed if validation fails.</param>
    /// <param name="contentTypes">The allowed content types for the FormFile.</param>
    /// <returns>Returns the rule builder options for the FormFile.</returns>
    public static IRuleBuilderOptions<TElement, IFormFile> FormFile<TElement>(this IRuleBuilder<TElement, IFormFile> ruleBuilder, string errorMessage, params string[] contentTypes) where TElement : class
    {
        return ruleBuilder.SetValidator(new FormFileGeneralValidator(errorMessage, ONE_KB, TWO_MB, contentTypes));
    }

    /// <summary>
    /// Adds a rule for validating an image FormFile, enforcing size limits and content types.
    /// </summary>
    /// <typeparam name="TElement">The type of the element being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder for the image FormFile.</param>
    /// <param name="errorMessage">The error message to be displayed if validation fails.</param>
    /// <param name="maxFileSize">The maximum allowed file size in bytes.</param>
    /// <param name="contentTypes">The allowed content types for the image FormFile.</param>
    /// <returns>Returns the rule builder options for the image FormFile.</returns>
    public static IRuleBuilderOptions<TElement, IFormFile> FormFile<TElement>(this IRuleBuilder<TElement, IFormFile> ruleBuilder, string errorMessage, int maxFileSize, params string[] contentTypes) where TElement : class
    {
        return ruleBuilder.SetValidator(new FormFileGeneralValidator(errorMessage, ONE_KB, maxFileSize, contentTypes));
    }

    /// <summary>
    /// Adds a rule for validating a FormFile, enforcing size limits, content types, and a minimum file size.
    /// </summary>
    /// <typeparam name="TElement">The type of the element being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder for the FormFile.</param>
    /// <param name="errorMessage">The error message to be displayed if validation fails.</param>
    /// <param name="minFileSize">The minimum allowed file size in bytes.</param>
    /// <param name="maxFileSize">The maximum allowed file size in bytes.</param>
    /// <param name="contentTypes">The allowed content types for the FormFile.</param>
    /// <returns>Returns the rule builder options for the FormFile.</returns>
    public static IRuleBuilderOptions<TElement, IFormFile> FormFile<TElement>(this IRuleBuilder<TElement, IFormFile> ruleBuilder, string errorMessage, int minFileSize, int maxFileSize, params string[] contentTypes) where TElement : class
    {
        return ruleBuilder.SetValidator(new FormFileGeneralValidator(errorMessage, minFileSize, maxFileSize, contentTypes));
    }
}