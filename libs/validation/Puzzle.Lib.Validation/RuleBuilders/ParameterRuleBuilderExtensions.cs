namespace Puzzle.Lib.Validation.RuleBuilders;

/// <summary>
/// Provides extension methods for creating integer validation rules using FluentValidation.
/// </summary>
public static class ParameterRuleBuilderExtensions
{
    ///<summary>
    /// Ensures that the validated string value is not null or empty, and adds a custom error message if it is.
    ///</summary>
    ///<param name="ruleBuilder">The FluentValidation IRuleBuilder instance being extended.</param>
    ///<param name="errorMessage">The custom error message to add if the value is null or empty.</param>
    ///<typeparam name="TElement">The type of the class being validated.</typeparam>
    ///<returns>The updated FluentValidation IRuleBuilderOptions instance.</returns>
    public static IRuleBuilderOptions<TElement, string> Ensure<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage)
    {
        return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage);
    }

    ///<summary>
    /// Ensures that the validated string value is not null or empty, and adds a custom error message if it is.
    ///</summary>
    ///<param name="ruleBuilder">The FluentValidation IRuleBuilder instance being extended.</param>
    ///<param name="errorMessage">The custom error message to add if the value is null or empty.</param>
    ///<param name="maximumLength"></param>
    ///<typeparam name="TElement">The type of the class being validated.</typeparam>
    ///<returns>The updated FluentValidation IRuleBuilderOptions instance.</returns>
    public static IRuleBuilderOptions<TElement, string> Ensure<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage, int maximumLength)
    {
        return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .MaximumLength(maximumLength).WithMessage(errorMessage);
    }

    ///<summary>
    /// Ensures that the validated byte[] value is not null or empty, and adds a custom error message if it is.
    ///</summary>
    ///<param name="ruleBuilder">The FluentValidation IRuleBuilder instance being extended.</param>
    ///<param name="errorMessage">The custom error message to add if the value is null or empty.</param>
    ///<typeparam name="TElement">The type of the class being validated.</typeparam>
    ///<returns>The updated FluentValidation IRuleBuilderOptions instance.</returns>
    public static IRuleBuilderOptions<TElement, byte[]> Ensure<TElement>(this IRuleBuilder<TElement, byte[]> ruleBuilder, string errorMessage) where TElement : class
    {
        return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage);
    }

    ///<summary>
    /// Ensures that the validated List<string> value is not null or empty, and adds a custom error message if it is.
    ///</summary>
    ///<param name="ruleBuilder">The FluentValidation IRuleBuilder instance being extended.</param>
    ///<param name="errorMessage">The custom error message to add if the value is null or empty.</param>
    ///<typeparam name="TClass">The type of the class being validated.</typeparam>
    ///<returns>The updated FluentValidation IRuleBuilderOptions instance.</returns>
    public static IRuleBuilderOptions<TElement, List<string>> Ensure<TElement>(this IRuleBuilder<TElement, List<string>> ruleBuilder, string errorMessage) where TElement : class
    {
        return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage);
    }

    /// <summary>
    /// Specifies a validation rule for an integer property using FluentValidation.
    /// </summary>
    /// <typeparam name="TElement">The type of the element being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder to which this extension is applied.</param>
    /// <param name="errorMessage">The error message to display if validation fails.</param>
    /// <returns>An <see cref="IRuleBuilderOptions{TElement, int}"/> object representing the validation rule.</returns>
    /// <remarks>
    /// This method sets a validation rule that ensures an integer property is inclusive between 1 and the maximum possible integer value.
    /// </remarks>
    public static IRuleBuilderOptions<TElement, int> Id<TElement>(this IRuleBuilder<TElement, int> ruleBuilder, string errorMessage) where TElement : class
    {
        return ruleBuilder.InclusiveBetween(1, int.MaxValue).WithMessage(errorMessage);
    }

    /// <summary>
    /// Defines a validation rule that checks if the value of an enumeration property is a valid enumeration value.
    /// </summary>
    /// <typeparam name="TElement">The type of the element being validated.</typeparam>
    /// <typeparam name="TProperty">The type of the property being validated, which should be an enum.</typeparam>
    /// <param name="ruleBuilder">The rule builder to which this validation rule is added.</param>
    /// <param name="errorMessage">The error message to be displayed if validation fails.</param>
    /// <returns>A rule builder options object that allows further configuration of the validation rule.</returns>
    /// <remarks>
    /// This rule checks if the value of the enumeration property is a valid enumeration value.
    /// </remarks>
    public static IRuleBuilderOptions<TElement, TProperty> Enum<TElement, TProperty>(this IRuleBuilder<TElement, TProperty> ruleBuilder, string errorMessage)
        where TElement : class
        where TProperty : Enum
    {
        return ruleBuilder.IsInEnum().WithMessage(errorMessage);
    }

    /// <summary>
    /// Configures a validation rule for a URL string using FluentValidation.
    /// </summary>
    /// <typeparam name="TElement">The type of the element being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder to which the validation rules are added.</param>
    /// <param name="errorMessage">The error message to be displayed if the validation fails.</param>
    /// <returns>The rule builder options for further configuration.</returns>
    public static IRuleBuilderOptions<TElement, string> Url<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage)
    {
        return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)).WithMessage(errorMessage);
    }

    public static IRuleBuilderOptions<TElement, string> Url<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage, int maximumLength)
    {
        return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)).WithMessage(errorMessage)
                .MaximumLength(maximumLength).WithMessage(errorMessage);
    }

}
