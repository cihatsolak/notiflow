namespace Puzzle.Lib.Validation.RuleBuilders
{
    /// <summary>
    /// Provides extension methods for building finance-related validation rules using FluentValidation library.
    /// </summary>
    public static class FinanceRuleBuilderOptionsExtensions
    {
        /// <summary>
        /// Adds a custom validation rule for credit card numbers to the current <see cref="IRuleBuilder{TClass, string}"/> instance.
        /// </summary>
        /// <typeparam name="TClass">The type of the class being validated.</typeparam>
        /// <param name="ruleBuilder">The current instance of <see cref="IRuleBuilder{TClass, string}"/> being extended.</param>
        /// <param name="errorMessage">The error message to be returned if the validation fails.</param>
        /// <returns>The current instance of <see cref="IRuleBuilderOptions{TClass, string}"/>.</returns>
        public static IRuleBuilderOptions<TClass, string> CustomCreditCard<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .Length(13, 19).WithMessage(errorMessage)
                .CreditCard()
                .Matches(RegularExpressions.CreditCard).WithMessage(errorMessage);
        }

        /// <summary>
        /// Adds a validation rule for credit or debit card numbers to the current <see cref="IRuleBuilder{TClass, string}"/> instance.
        /// </summary>
        /// <typeparam name="TClass">The type of the class being validated.</typeparam>
        /// <param name="ruleBuilder">The current instance of <see cref="IRuleBuilder{TClass, string}"/> being extended.</param>
        /// <param name="errorMessage">The error message to be returned if the validation fails.</param>
        /// <returns>The current instance of <see cref="IRuleBuilderOptions{TClass, string}"/>.</returns>
        public static IRuleBuilderOptions<TClass, string> CreditOrDebitCard<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .Length(10, 19).WithMessage(errorMessage)
                .Matches(RegularExpressions.CreditCard).WithMessage(errorMessage);
        }
    }
}
