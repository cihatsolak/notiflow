namespace Puzzle.Lib.Social.Infrastructure.Models;

public sealed record GoogleUserResponse
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required bool EmailVerified { get; init; }
    public required string ProfilePicture { get; init; }
    public required string LoginProviderSubject { get; init; }
}
