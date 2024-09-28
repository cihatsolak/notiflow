namespace Puzzle.Lib.Social.Infrastructure;

/// <summary>
/// Represents the response received from Google when retrieving user information.
/// </summary>
public sealed record GoogleUserResponse
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the email address has been verified.
    /// </summary>
    public required bool EmailVerified { get; init; }

    /// <summary>
    /// Gets or sets the URL of the user's profile picture.
    /// </summary>
    public string ProfilePicture { get; init; }

    /// <summary>
    /// Gets or sets the subject identifier provided by the login provider.
    /// </summary>
    public required string LoginProviderSubject { get; init; }
}
