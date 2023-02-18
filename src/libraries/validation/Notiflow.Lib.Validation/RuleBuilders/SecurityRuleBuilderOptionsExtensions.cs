namespace Notiflow.Lib.Validation.RuleBuilders
{
    public static class SecurityRuleBuilderOptionsExtensions
    {
        /// <summary>
        /// Checks if the user's password is secure
        /// </summary>
        /// <typeparam name="TClass">class to validate</typeparam>
        /// <param name="ruleBuilder">rule builder</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>type of rule builder options</returns>
        public static IRuleBuilderOptions<TClass, string> StrongPassword<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .MinimumLength(8).WithMessage(errorMessage)
                .Matches(RegularExpressions.Password).WithMessage(errorMessage);
        }
    }
}
