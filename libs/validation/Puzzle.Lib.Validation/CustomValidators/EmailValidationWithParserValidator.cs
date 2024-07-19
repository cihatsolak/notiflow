namespace Puzzle.Lib.Validation.CustomValidators;

/// <summary>
/// Validates an email address or a list of comma-separated email addresses with a specified parser character.
/// </summary>
/// <remarks>
/// This class uses regular expressions to validate the format of the email address and also checks the top-level domain (TLD) using an extension method.
/// </remarks>
internal class EmailValidationWithParserValidator : AbstractValidator<string>
{
    readonly char[] parsers = [',', '.', ';', ':', '-', '/'];

    internal EmailValidationWithParserValidator(char parser, string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
            throw new ArgumentException("Invalid error message.");

        if (char.IsLetter(parser) || Array.Exists(parsers, p => p == parser))
            throw new ArgumentException("Define a valid parser.");

        RuleFor(email => email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(errorMessage)
            .NotNull().WithMessage(errorMessage);

        RuleFor(email => email)
            .Cascade(CascadeMode.Stop)
            .Must(email => ValidateEmails(email, parser)).WithMessage(errorMessage);
    }

    /// <summary>
    /// Validates a list of email addresses separated by a specified parser character.
    /// </summary>
    /// <remarks>
    /// This method uses regular expressions to validate the format of the email address and also checks the top-level domain (TLD) using an extension method.
    /// </remarks>
    /// <param name="emails">The list of email addresses separated by the specified parser character.</param>
    /// <param name="parser">The parser character used to separate the list of email addresses.</param>
    /// <returns>A boolean value indicating whether the list of email addresses is valid.</returns>
    private static bool ValidateEmails(string emails, char parser)
    {
        var splittedEmails = emails.Split(parser, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return Array.TrueForAll(splittedEmails, email => RegularExpressions.Email.IsMatch(email) && EmailValidateExtension.ValidateTld(email));
    }
}
