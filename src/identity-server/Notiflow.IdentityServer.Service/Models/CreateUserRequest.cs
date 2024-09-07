namespace Notiflow.IdentityServer.Service.Models;

public sealed record CreateUserRequest(string Name, string Surname, string Email, string Username, string Password);

public sealed class CreateUserRequestExample : IExamplesProvider<CreateUserRequest>
{
    public CreateUserRequest GetExamples() => new("John", "Doe", "john.doe@example.com", "StarryTraveler92", "X7v!j2a$L9");
}

public sealed class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(p => p.Name)
            .Ensure(FluentVld.Errors.USER_NAME)
            .Length(2, 100).WithMessage(FluentVld.Errors.USER_NAME);

        RuleFor(p => p.Surname)
            .Ensure(FluentVld.Errors.USER_SURNAME)
            .Length(2, 100).WithMessage(FluentVld.Errors.USER_SURNAME);

        RuleFor(p => p.Email).Email(FluentVld.Errors.EMAIL);

        RuleFor(p => p.Username)
            .Ensure(FluentVld.Errors.USERNAME)
            .Length(5, 100).WithMessage(FluentVld.Errors.USERNAME);

        RuleFor(p => p.Password).StrongPassword(FluentVld.Errors.PASSWORD, FluentVld.Rules.PASSWORD_MAX_100_LENGTH);
    }
}