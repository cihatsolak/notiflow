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
        ///<typeparam name="TElement">The type of the class being validated.</typeparam>
        ///<returns>The updated FluentValidation IRuleBuilderOptions instance.</returns>
        public static IRuleBuilderOptions<TElement, string> NotNullAndNotEmpty<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage)
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
        ///<typeparam name="TElement">The type of the class being validated.</typeparam>
        ///<returns>The updated FluentValidation IRuleBuilderOptions instance.</returns>
        public static IRuleBuilderOptions<TElement, byte[]> NotNullAndNotEmpty<TElement>(this IRuleBuilder<TElement, byte[]> ruleBuilder, string errorMessage) where TElement : class, new()
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
        public static IRuleBuilderOptions<TElement, List<string>> NotNullAndNotEmpty<TElement>(this IRuleBuilder<TElement, List<string>> ruleBuilder, string errorMessage) where TElement : class, new()
        {
            return ruleBuilder
                    .NotEmpty().WithMessage(errorMessage)
                    .NotNull().WithMessage(errorMessage);
        }
    }
}
