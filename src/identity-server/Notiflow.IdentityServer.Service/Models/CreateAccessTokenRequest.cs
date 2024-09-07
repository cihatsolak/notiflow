namespace Notiflow.IdentityServer.Service.Models;

public sealed record CreateAccessTokenRequest(string Username, string Password);

public sealed class CreateAccessTokenRequestExample : IExamplesProvider<CreateAccessTokenRequest>
{
    public CreateAccessTokenRequest GetExamples() => new("StarryTraveler92", "X7v!j2a$L9");
}

public sealed class CreateAccessTokenRequestValidator : AbstractValidator<CreateAccessTokenRequest>
{
    public CreateAccessTokenRequestValidator()
    {
        RuleFor(p => p.Username)
            .Ensure(FluentVld.Errors.USERNAME)
            .Length(5, 100).WithMessage(FluentVld.Errors.USERNAME);

        RuleFor(p => p.Password).StrongPassword(FluentVld.Errors.PASSWORD, FluentVld.Rules.PASSWORD_MAX_100_LENGTH);
    }
}