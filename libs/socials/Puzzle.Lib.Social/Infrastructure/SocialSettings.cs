namespace Puzzle.Lib.Social.Infrastructure;

public sealed record SocialSettings
{
    public GoogleAuthConfig GoogleAuthConfig { get; set; }
}

public sealed record GoogleAuthConfig
{
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }
}
