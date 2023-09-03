namespace Puzzle.Lib.Social.Models;

public sealed record GoogleLoginRequest
{
    [JsonRequired]
    public required string IdToken { get; init; }
}
