namespace Notiflow.Panel.Models;

public sealed record TokenResponse
{
    [JsonRequired]
    public required string AccessToken { get; init; }
    public required DateTime AccessTokenExpiration { get; init; }
    public required int ExpiresIn { get; init; }
    public required string RefreshToken { get; init; }
    public required DateTime RefreshTokenExpiration { get; init; }
}
