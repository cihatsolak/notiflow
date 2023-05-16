﻿namespace Puzzle.Lib.Validation.RuleBuilders
{
    /// <summary>
    /// Contains extension methods for building validation rules for personal information such as TC number, landline and mobile phone.
    /// </summary>
    public static class PersonalRuleBuilderOptionsExtensions
    {
        /// <summary>
        /// Adds validation rules for a TC number.
        /// </summary>
        /// <typeparam name="TElement">The type of the class being validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="errorMessage">The error message to display if the validation fails.</param>
        /// <returns>The rule builder options.</returns>
        public static IRuleBuilderOptions<TElement, string> TcNumber<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage) where TElement : class
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
        /// <typeparam name="TElement">The type of the class being validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="errorMessage">The error message to display if the validation fails.</param>
        /// <returns>The rule builder options.</returns>
        public static IRuleBuilderOptions<TElement, string> Landline<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage) where TElement : class
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
        /// <typeparam name="TElement">The type of the class being validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="errorMessage">The error message to display if the validation fails.</param>
        /// <returns>The rule builder options.</returns>
        public static IRuleBuilderOptions<TElement, string> MobilePhone<TElement>(this IRuleBuilder<TElement, string> ruleBuilder, string errorMessage) where TElement : class
        {
            return ruleBuilder
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage)
                .Length(10).WithMessage(errorMessage)
                .Matches(RegularExpressions.MobilePhone).WithMessage(errorMessage);
        }

        /// <summary>
        /// Specifies a validation rule for the birth date of an entity, based on the minimum and maximum age.
        /// The birth date must be within the specified age range.
        /// </summary>
        /// <typeparam name="TElement">The type of the entity being validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="minimumAge">The minimum age allowed for the birth date.</param>
        /// <param name="maximumAge">The maximum age allowed for the birth date.</param>
        /// <param name="errorMessage">The error message to be displayed if the validation fails.</param>
        /// <returns>The rule builder with the birth date validation rule added.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the rule builder is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the minimum or maximum age is not a positive value.</exception>
        public static IRuleBuilderOptions<TElement, DateTime> BirthDate<TElement>(this IRuleBuilder<TElement, DateTime> ruleBuilder, string errorMessage, int minimumAge = 18, int maximumAge = 70) where TElement : class
        {
            return ruleBuilder
                .Must(birthDate => (birthDate >= DateTime.Today || birthDate.Year < DateTime.Today.Year - maximumAge || birthDate.Year > DateTime.Today.Year - minimumAge))
                .WithMessage(errorMessage);
        }
    }
}
