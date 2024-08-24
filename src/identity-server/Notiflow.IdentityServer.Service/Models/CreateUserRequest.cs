namespace Notiflow.IdentityServer.Service.Models;

public sealed record CreateUserRequest(string Name, string Surname, string Email, string Username, string Password);

public sealed class CreateUserRequestExample : IExamplesProvider<CreateUserRequest>
{
    public CreateUserRequest GetExamples() => new("John", "Doe", "john.doe@example.com", "StarryTraveler92", "X7v!j2a$L9");
}

public sealed class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    private const int PASSWORD_MAX_LENGTH = 100;

    public CreateUserRequestValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Name)
            .Ensure(localizer[ValidationErrorMessage.USER_NAME])
            .Length(2, 100).WithMessage(localizer[ValidationErrorMessage.USER_NAME]);

        RuleFor(p => p.Surname)
            .Ensure(localizer[ValidationErrorMessage.USER_SURNAME])
            .Length(2, 100).WithMessage(localizer[ValidationErrorMessage.USER_SURNAME]);

        RuleFor(p => p.Email).Email(localizer[ValidationErrorMessage.EMAIL]);

        RuleFor(p => p.Username)
            .Ensure(localizer[ValidationErrorMessage.USERNAME])
            .Length(5, 100).WithMessage(localizer[ValidationErrorMessage.USERNAME]);

        RuleFor(p => p.Password).StrongPassword(localizer[ValidationErrorMessage.PASSWORD], PASSWORD_MAX_LENGTH);
    }
}