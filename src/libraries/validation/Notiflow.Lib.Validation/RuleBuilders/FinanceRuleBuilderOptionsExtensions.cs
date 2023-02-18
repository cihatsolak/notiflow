namespace Notiflow.Lib.Validation.RuleBuilders
{
    public static class FinanceRuleBuilderOptionsExtensions
    {
        /// <summary>
        /// Checks if the user's credit card number is correct
        /// </summary>
        /// <typeparam name="TClass">class to validate</typeparam>
        /// <param name="ruleBuilder">rule builder</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>type of rule builder options</returns>
        public static IRuleBuilderOptions<TClass, string> CreditCard<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .Length(13, 19).WithMessage(errorMessage)
                .Matches(RegularExpressions.CreditCard).WithMessage(errorMessage);
        }

        /// <summary>
        /// Checks if the tax number is correct
        /// </summary>
        /// <typeparam name="TClass">class to validate</typeparam>
        /// <param name="ruleBuilder">rule builder</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>type of rule builder options</returns>
        public static IRuleBuilderOptions<TClass, string> CreditOrDebitCard<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .Length(10).WithMessage(errorMessage)
                .Matches(RegularExpressions.CreditCard).WithMessage(errorMessage);
        }
    }
}
