namespace Puzzle.Lib.Validation.AbstractValidators
{
    internal class EmailValidationWithParserValidator : AbstractValidator<string>
    {
        readonly char[] parsers = new char[] { ',', '.', ';', ':', '-', '/' };

        internal EmailValidationWithParserValidator(char parser, string errorMessage)
        {
            if (char.IsLetter(parser))
                throw new ArgumentException("Define a valid parser.");

            if (string.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentException("Invalid error message.");

            if (!parsers.Any(p => p == parser))
                throw new ArgumentException("Define a valid parser.");

            RuleFor(x => x)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(errorMessage)
                .NotNull().WithMessage(errorMessage);

            RuleFor(email => email)
                .Cascade(CascadeMode.Stop)
                .Must(email => ValidateEmails(email, parser)).WithMessage(errorMessage);
        }

        private static bool ValidateEmails(string emails, char parser)
        {
            return emails.Split(parser, StringSplitOptions.RemoveEmptyEntries).All(email => RegularExpressions.Email.IsMatch(email) && EmailValidateExtension.ValidateTld(email));
        }
    }
}
