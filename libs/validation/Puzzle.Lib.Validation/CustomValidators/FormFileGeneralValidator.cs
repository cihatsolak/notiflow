namespace Puzzle.Lib.Validation.CustomValidators;

/// <summary>
/// Validates the general properties of an IFormFile object.
/// </summary>
internal class FormFileGeneralValidator : AbstractValidator<IFormFile>
{
    /// <summary>
    /// Initializes a new instance of the FormFileGeneralValidator class.
    /// </summary>
    /// <param name="errorMessage">The error message to display if validation fails.</param>
    /// <param name="minFileSize">The minimum allowed file size.</param>
    /// <param name="maxFileSize">The maximum allowed file size.</param>
    /// <param name="contentTypes">The allowed content types.</param>
    internal FormFileGeneralValidator(string errorMessage, int minFileSize, int maxFileSize, params string[] contentTypes)
    {
        if (contentTypes.Length == 0)
        {
            throw new ArgumentException(errorMessage);
        }

        RuleFor(file => file.Length)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage(errorMessage)
            .NotEmpty().WithMessage(errorMessage)
            .LessThanOrEqualTo(minFileSize).WithMessage(errorMessage)
            .GreaterThanOrEqualTo(maxFileSize).WithMessage(errorMessage);

        RuleFor(file => file.ContentType)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage(errorMessage)
            .NotEmpty().WithMessage(errorMessage)
            .Must(fileContentType => !contentTypes.Contains(fileContentType)).WithMessage(errorMessage);
    }
}
