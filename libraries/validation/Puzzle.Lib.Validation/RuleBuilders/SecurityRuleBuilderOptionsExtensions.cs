namespace Puzzle.Lib.Validation.RuleBuilders
{
    /// <summary>
    /// Contains extension methods for building validation rules for security related information such as passwords.
    /// </summary>
    public static class SecurityRuleBuilderOptionsExtensions
    {
        /// <summary>
        /// Adds validation rules for a strong password.
        /// </summary>
        /// <typeparam name="TElement">The type of the class being validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="errorMessage">The error message to display if the validation fails.</param>
        /// <returns>The rule builder options.</returns>
        public static IRuleBuilderOptions<TElement, string> StrongPassword<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage) where TElement : class
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .MinimumLength(8).WithMessage(errorMessage)
                .Matches(RegularExpressions.Password).WithMessage(errorMessage);
        }
    }
}
