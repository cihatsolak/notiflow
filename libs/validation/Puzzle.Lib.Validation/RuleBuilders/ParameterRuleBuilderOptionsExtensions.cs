namespace Puzzle.Lib.Validation.RuleBuilders;

/// <summary>
/// Provides extension methods for creating integer validation rules using FluentValidation.
/// </summary>
public static class ParameterRuleBuilderOptionsExtensions
{
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
}
