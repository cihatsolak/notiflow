namespace Puzzle.Lib.Auth.Infrastructure.Settings
{
    public interface IJwtTokenSetting
    {
        List<string> Audiences { get; init; }
        string Issuer { get; init; }
        int AccessTokenExpiration { get; init; }
        int RefreshTokenExpiration { get; init; }
        string SecurityKey { get; init; }
    }

    internal sealed record JwtTokenSetting : IJwtTokenSetting
    {
        public List<string> Audiences { get; init; }
        public string Issuer { get; init; }
        public int AccessTokenExpiration { get; init; }
        public int RefreshTokenExpiration { get; init; }
        public string SecurityKey { get; init; }
    }
}
