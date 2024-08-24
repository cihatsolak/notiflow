namespace Notiflow.IdentityServer.Service.Models;

public sealed record CreateAccessTokenRequest(string Username, string Password);

public sealed class CreateAccessTokenRequestExample : IExamplesProvider<CreateAccessTokenRequest>
{
    public CreateAccessTokenRequest GetExamples() => new("StarryTraveler92", "X7v!j2a$L9");
}

public sealed class CreateAccessTokenRequestValidator : AbstractValidator<CreateAccessTokenRequest>
{
     private const int PASSWORD_MAX_LENGTH = 100;

    public CreateAccessTokenRequestValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Username)
            .Ensure(localizer[ValidationErrorMessage.USERNAME])
            .Length(5, 100).WithMessage(localizer[ValidationErrorMessage.USERNAME]);

        RuleFor(p => p.Password).StrongPassword(localizer[ValidationErrorMessage.PASSWORD], PASSWORD_MAX_LENGTH);
    }
}