namespace Notiflow.IdentityServer.Service.Models.Auths;

public sealed record CreateAccessTokenRequest
{
    public string Username { get; init; }
    public string Password { get; init; }
}

public sealed class CreateAccessTokenRequestValidator : AbstractValidator<CreateAccessTokenRequest>
{
    public CreateAccessTokenRequestValidator()
    {
        RuleFor(p => p.Username)
            .NotNullAndNotEmpty("-1")
            .Length(5, 100);

        RuleFor(p => p.Password)
            .NotNullAndNotEmpty("-1")
            .StrongPassword("-1")
            .MaximumLength(100).WithMessage("-1");
    }
}