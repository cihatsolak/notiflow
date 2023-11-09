namespace Puzzle.Lib.Validation.CustomValidators;

internal class FormFileGeneralValidator : AbstractValidator<IFormFile>
{
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