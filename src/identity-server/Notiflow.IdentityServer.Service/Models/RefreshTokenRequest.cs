namespace Notiflow.IdentityServer.Service.Models;

public sealed record RefreshTokenRequest(string Token);

public sealed class RefreshTokenRequestExample : IExamplesProvider<RefreshTokenRequest>
{
    public RefreshTokenRequest GetExamples() => new("8xLOxBtZp8ss1238xLOxBtZp8");
}

public sealed class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(p => p.Token)
            .Ensure(FluentVld.Errors.REFRESH_TOKEN)
            .Length(45, 55).WithMessage(FluentVld.Errors.REFRESH_TOKEN);
    }
}