namespace Puzzle.Lib.Auth.Infrastructure.Settings
{
    internal sealed record JwtTokenSetting
    {
        public List<string> Audiences { get; init; }
        public string Issuer { get; init; }
        public int AccessTokenExpiration { get; init; }
        public int RefreshTokenExpiration { get; init; }
        public string SecurityKey { get; init; }
    }
}
