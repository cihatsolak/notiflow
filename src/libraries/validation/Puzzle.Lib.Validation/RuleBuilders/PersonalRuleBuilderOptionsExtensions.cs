namespace Puzzle.Lib.Validation.RuleBuilders
{
    /// <summary>
    /// Contains extension methods for building validation rules for personal information such as TC number, landline and mobile phone.
    /// </summary>
    public static class PersonalRuleBuilderOptionsExtensions
    {
        /// <summary>
        /// Adds validation rules for a TC number.
        /// </summary>
        /// <typeparam name="TClass">The type of the class being validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="errorMessage">The error message to display if the validation fails.</param>
        /// <returns>The rule builder options.</returns>
        public static IRuleBuilderOptions<TClass, string> TcNumber<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .Length(11).WithMessage(errorMessage)
                .Matches(RegularExpressions.TcNumber).WithMessage(errorMessage);
        }

        /// <summary>
        /// Adds validation rules for a landline phone number.
        /// </summary>
        /// <typeparam name="TClass">The type of the class being validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="errorMessage">The error message to display if the validation fails.</param>
        /// <returns>The rule builder options.</returns>
        public static IRuleBuilderOptions<TClass, string> Landline<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .Length(11).WithMessage(errorMessage)
                .Matches(RegularExpressions.Landline).WithMessage(errorMessage);
        }

        /// <summary>
        /// Adds validation rules for a mobile phone number.
        /// </summary>
        /// <typeparam name="TClass">The type of the class being validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="errorMessage">The error message to display if the validation fails.</param>
        /// <returns>The rule builder options.</returns>
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
