namespace Puzzle.Lib.Social.Infrastructure;

public sealed record SocialSettings
{
    public GoogleAuthConfig GoogleAuthConfig { get; init; }
    public FacebookAuthConfig FacebookAuthConfig { get; init; }
}

public sealed record GoogleAuthConfig
{
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
}

public sealed record FacebookAuthConfig
{
    public required string BaseUrl { get; init; }
    public required string TokenValidationUrl { get; init; }
    public required string UserInfoUrl { get; init; }
    public required string AppId { get; init; }
    public required string AppSecret { get; init; }
}