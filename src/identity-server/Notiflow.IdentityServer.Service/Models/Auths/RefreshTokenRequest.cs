namespace Notiflow.IdentityServer.Service.Models.Auths;

public sealed record RefreshTokenRequest
{
    public string Token { get; init; }
}

public sealed class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(p => p.Token)
            .Length(45, 55).WithMessage("error");
    }
}