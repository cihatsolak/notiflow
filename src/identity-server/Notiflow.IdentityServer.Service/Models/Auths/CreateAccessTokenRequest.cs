namespace Notiflow.IdentityServer.Service.Models.Auths;

//public sealed record CreateAccessTokenRequest(string Username, string Password);

public sealed record CreateAccessTokenRequest
{
    public string Username { get; init; }
    public string Password { get; init; }
}

public sealed class CreateAccessTokenRequestValidator : AbstractValidator<CreateAccessTokenRequest>
{
    public CreateAccessTokenRequestValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.Username)
            .NotNullAndNotEmpty(localizer[ResultState.USERNAME])
            .Length(5, 100).WithMessage(localizer[ResultState.PASSWORD]);

        RuleFor(p => p.Password)
            .StrongPassword(localizer[ResultState.PASSWORD])
            .MaximumLength(100).WithMessage(localizer[ResultState.PASSWORD]);
    }
}