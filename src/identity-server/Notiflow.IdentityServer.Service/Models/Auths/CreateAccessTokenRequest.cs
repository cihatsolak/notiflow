namespace Notiflow.IdentityServer.Service.Models.Auths;

//public sealed record CreateAccessTokenRequest(string Username, string Password);

public sealed record CreateAccessTokenRequest
{
    public string Username { get; init; }
    public string Password { get; init; }
}

public sealed class CreateAccessTokenRequestValidator : AbstractValidator<CreateAccessTokenRequest>
{
    public CreateAccessTokenRequestValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleFor(p => p.Username)
            .NotNullAndNotEmpty(localizer[ValidationErrorCodes.USERNAME])
            .Length(5, 100).WithMessage(localizer[ValidationErrorCodes.PASSWORD]);

        RuleFor(p => p.Password)
            .StrongPassword(localizer[ValidationErrorCodes.PASSWORD])
            .MaximumLength(100).WithMessage(localizer[ValidationErrorCodes.PASSWORD]);
    }
}