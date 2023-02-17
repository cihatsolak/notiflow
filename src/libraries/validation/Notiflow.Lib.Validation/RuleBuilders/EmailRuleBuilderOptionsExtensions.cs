namespace Notiflow.Lib.Validation.RuleBuilders
{
    public static class EmailRuleBuilderOptionsExtensions
    {
        /// <summary>
        /// Check that the email address is correct
        /// </summary>
        /// <typeparam name="TClass">class to validate</typeparam>
        /// <param name="ruleBuilder">rule builder</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>type of rule builder options</returns>
        public static IRuleBuilderOptions<TClass, string> Email<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage(errorMessage)
                .Matches(RegularExpressions.Email).WithMessage(errorMessage);
        }
    }
}
