namespace Notiflow.Lib.Validation.RuleBuilders
{
    public static class NullEmptyRuleBuilderOptionsExtensions
    {
        /// <summary>
        /// Checks if string type is null or empty
        /// </summary>
        /// <typeparam name="TClass">class to validate</typeparam>
        /// <param name="ruleBuilder">rule builder</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>type of rule builder options</returns>
        public static IRuleBuilderOptions<TClass, string> NotNullAndNotEmpty<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                    .NotEmpty().WithMessage(errorMessage)
                    .NotNull().WithMessage(errorMessage);
        }

        /// <summary>
        /// Check if byte array is null or empty
        /// </summary>
        /// <typeparam name="TClass">class to validate</typeparam>
        /// <param name="ruleBuilder">rule builder</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>type of rule builder options</returns>
        public static IRuleBuilderOptions<TClass, byte[]> NotNullAndNotEmpty<TClass>(this IRuleBuilder<TClass, byte[]> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                    .NotEmpty().WithMessage(errorMessage)
                    .NotNull().WithMessage(errorMessage);
        }

        /// <summary>
        /// Checks if string list is empty or null
        /// </summary>
        /// <typeparam name="TClass">class to validate</typeparam>
        /// <param name="ruleBuilder">rule builder</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>type of rule builder options</returns>
        public static IRuleBuilderOptions<TClass, List<string>> NotNullAndNotEmpty<TClass>(this IRuleBuilder<TClass, List<string>> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                    .NotEmpty().WithMessage(errorMessage)
                    .NotNull().WithMessage(errorMessage);
        }
    }
}
