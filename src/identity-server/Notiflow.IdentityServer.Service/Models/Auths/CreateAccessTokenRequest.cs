namespace Notiflow.IdentityServer.Service.Models.Auths;

public sealed record CreateAccessTokenRequest(string Username, string Password);

public sealed class CreateAccessTokenRequestValidator : AbstractValidator<CreateAccessTokenRequest>
{
    public CreateAccessTokenRequestValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Username)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.USERNAME])
            .Length(5, 100).WithMessage(localizer[ValidationErrorMessage.USERNAME]);

        RuleFor(p => p.Password)
            .StrongPassword(localizer[ValidationErrorMessage.PASSWORD])
            .MaximumLength(100).WithMessage(localizer[ValidationErrorMessage.PASSWORD]);
    }
}