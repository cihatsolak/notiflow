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
}
