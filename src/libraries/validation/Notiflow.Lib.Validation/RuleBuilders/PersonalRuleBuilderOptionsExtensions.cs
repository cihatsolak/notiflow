namespace Notiflow.Lib.Validation.RuleBuilders
{
    public static class PersonalRuleBuilderOptionsExtensions
    {
        /// <summary>
        /// Checks if the user's ID number is correct
        /// </summary>
        /// <typeparam name="TClass">class to validate</typeparam>
        /// <param name="ruleBuilder">rule builder</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>type of rule builder options</returns>
        public static IRuleBuilderOptions<TClass, string> TcNumber<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .Length(11).WithMessage(errorMessage)
                .Matches(RegularExpressions.TcNumber).WithMessage(errorMessage);
        }

        /// <summary>
        /// Checks if the user's landline number is correct
        /// </summary>
        /// <remarks>The area code is included and the number must begin with a zero.</remarks>
        /// <typeparam name="TClass">class to validate</typeparam>
        /// <param name="ruleBuilder">rule builder</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>type of rule builder options</returns>
        public static IRuleBuilderOptions<TClass, string> Landline<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .Length(11).WithMessage(errorMessage)
                .Matches(RegularExpressions.Landline).WithMessage(errorMessage);
        }

        /// <summary>
        /// Checks if the user's mobiel phone number is correct
        /// </summary>
        /// <remarks>The area code is included and the number must begin with a zero.</remarks>
        /// <typeparam name="TClass">class to validate</typeparam>
        /// <param name="ruleBuilder">rule builder</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>type of rule builder options</returns>
        public static IRuleBuilderOptions<TClass, string> MobilePhone<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .Length(10).WithMessage(errorMessage)
                .Matches(RegularExpressions.MobilePhone).WithMessage(errorMessage);
        }
    }
}
