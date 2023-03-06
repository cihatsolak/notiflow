namespace Puzzle.Lib.Validation.RuleBuilders
{
    public static class EmailRuleBuilderOptionsExtensions
    {
        /// <summary>
        /// Check that the email address is correct
        /// </summary>
        /// <typeparam name="TElement">class to validate</typeparam>
        /// <param name="ruleBuilder">rule builder</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>type of rule builder options</returns>
        public static IRuleBuilderOptions<TElement, string> Email<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage) where TElement : class, new()
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage(errorMessage)
                .Matches(RegularExpressions.Email).WithMessage(errorMessage);
        }

        /// <summary>
        /// Check that the email address is correct
        /// </summary>
        /// <typeparam name="TElement">class to validate</typeparam>
        /// <param name="ruleBuilder">rule builder</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>type of rule builder options</returns>
        public static IRuleBuilderOptions<TElement, string> EmailListWithSemicolon<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage) where TElement : class, new()
        {
            return ruleBuilder.SetValidator(new EmailValidationWithParserValidator(';', errorMessage));
        }
    }
}
