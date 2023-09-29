namespace Puzzle.Lib.Social.Infrastructure;

public sealed record SocialSettings
{
    public GoogleAuthConfig GoogleAuthConfig { get; set; }
    public FacebookAuthConfig FacebookAuthConfig { get; set; }
}

public sealed record GoogleAuthConfig
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}

public sealed record FacebookAuthConfig
{
    public string BaseUrl { get; set; }
    public string TokenValidationUrl { get; set; }
    public string UserInfoUrl { get; set; }
    public string AppId { get; set; }
    public string AppSecret { get; set; }
}