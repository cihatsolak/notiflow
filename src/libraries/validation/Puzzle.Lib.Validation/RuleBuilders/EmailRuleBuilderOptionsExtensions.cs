namespace Puzzle.Lib.Validation.RuleBuilders
{
    /// <summary>
    /// Extension methods for building email validation rules using FluentValidation.
    /// </summary>
    public static class EmailRuleBuilderOptionsExtensions
    {
        /// <summary>
        /// Adds a validation rule for a single email address.
        /// </summary>
        /// <typeparam name="TElement">The type of the object being validated.</typeparam>
        /// <param name="ruleBuilder">The <see cref="IRuleBuilder{TElement, TProperty}"/> instance.</param>
        /// <param name="errorMessage">The error message to display if the validation fails.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{TElement, TProperty}"/> instance.</returns>
        public static IRuleBuilderOptions<TElement, string> Email<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage) where TElement : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage(errorMessage)
                .Matches(RegularExpressions.Email).WithMessage(errorMessage);
        }

        /// <summary>
        /// Adds a validation rule for a list of email addresses separated by semicolons.
        /// </summary>
        /// <typeparam name="TElement">The type of the object being validated.</typeparam>
        /// <param name="ruleBuilder">The <see cref="IRuleBuilder{TElement, TProperty}"/> instance.</param>
        /// <param name="errorMessage">The error message to display if the validation fails.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{TElement, TProperty}"/> instance.</returns>
        public static IRuleBuilderOptions<TElement, string> EmailListWithSemicolon<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage) where TElement : class, new()
        {
            return ruleBuilder.SetValidator(new EmailValidationWithParserValidator(';', errorMessage));
        }
    }
}
