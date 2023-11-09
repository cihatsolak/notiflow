namespace Notiflow.IdentityServer.Service.Models.Auths;

public sealed record RefreshTokenRequest(string Token);

public sealed class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.REFRESH_TOKEN])
            .Length(45, 55).WithMessage(localizer[ValidationErrorMessage.REFRESH_TOKEN]);
    }
}