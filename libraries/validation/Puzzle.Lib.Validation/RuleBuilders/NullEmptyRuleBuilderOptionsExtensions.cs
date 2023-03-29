namespace Puzzle.Lib.Validation.RuleBuilders
{
    ///<summary>
    /// Extension methods for FluentValidation IRuleBuilderOptions to ensure that string, byte[] and List<string> values are not null or empty.
    ///</summary>
    public static class NullEmptyRuleBuilderOptionsExtensions
    {
        ///<summary>
        /// Ensures that the validated string value is not null or empty, and adds a custom error message if it is.
        ///</summary>
        ///<param name="ruleBuilder">The FluentValidation IRuleBuilder instance being extended.</param>
        ///<param name="errorMessage">The custom error message to add if the value is null or empty.</param>
        ///<typeparam name="TClass">The type of the class being validated.</typeparam>
        ///<returns>The updated FluentValidation IRuleBuilderOptions instance.</returns>
        public static IRuleBuilderOptions<TClass, string> NotNullAndNotEmpty<TClass>(this IRuleBuilder<TClass, string> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                    .NotEmpty().WithMessage(errorMessage)
                    .NotNull().WithMessage(errorMessage);
        }

        ///<summary>
        /// Ensures that the validated byte[] value is not null or empty, and adds a custom error message if it is.
        ///</summary>
        ///<param name="ruleBuilder">The FluentValidation IRuleBuilder instance being extended.</param>
        ///<param name="errorMessage">The custom error message to add if the value is null or empty.</param>
        ///<typeparam name="TClass">The type of the class being validated.</typeparam>
        ///<returns>The updated FluentValidation IRuleBuilderOptions instance.</returns>
        public static IRuleBuilderOptions<TClass, byte[]> NotNullAndNotEmpty<TClass>(this IRuleBuilder<TClass, byte[]> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                    .NotEmpty().WithMessage(errorMessage)
                    .NotNull().WithMessage(errorMessage);
        }

        ///<summary>
        /// Ensures that the validated List<string> value is not null or empty, and adds a custom error message if it is.
        ///</summary>
        ///<param name="ruleBuilder">The FluentValidation IRuleBuilder instance being extended.</param>
        ///<param name="errorMessage">The custom error message to add if the value is null or empty.</param>
        ///<typeparam name="TClass">The type of the class being validated.</typeparam>
        ///<returns>The updated FluentValidation IRuleBuilderOptions instance.</returns>
        public static IRuleBuilderOptions<TClass, List<string>> NotNullAndNotEmpty<TClass>(this IRuleBuilder<TClass, List<string>> ruleBuilder, string errorMessage) where TClass : class, new()
        {
            return ruleBuilder
                    .NotEmpty().WithMessage(errorMessage)
                    .NotNull().WithMessage(errorMessage);
        }
    }
}
