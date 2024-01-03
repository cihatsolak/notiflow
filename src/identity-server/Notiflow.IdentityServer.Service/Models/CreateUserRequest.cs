namespace Notiflow.IdentityServer.Service.Models;

public sealed record CreateUserRequest(string Name, string Surname, string Email, string Username, string Password);

public sealed class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Name)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.USER_NAME])
            .Length(2, 100).WithMessage(localizer[ValidationErrorMessage.USER_NAME]);

        RuleFor(p => p.Surname)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.USER_SURNAME])
            .Length(2, 100).WithMessage(localizer[ValidationErrorMessage.USER_SURNAME]);

        RuleFor(p => p.Email)
            .Email(localizer[ValidationErrorMessage.EMAIL])
            .Length(5, 150).WithMessage(localizer[ValidationErrorMessage.EMAIL]);

        RuleFor(p => p.Username)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.USERNAME])
            .Length(5, 100).WithMessage(localizer[ValidationErrorMessage.USERNAME]);

        RuleFor(p => p.Password)
           .StrongPassword(localizer[ValidationErrorMessage.PASSWORD])
           .MaximumLength(200).WithMessage(localizer[ValidationErrorMessage.PASSWORD]);
    }
}