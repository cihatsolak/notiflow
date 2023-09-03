namespace Puzzle.Lib.Social.Services;

/// <summary>
///   Interface for Google authentication service.
/// </summary>
public interface IGoogleAuthService
{
    /// <summary>
    ///   Retrieves user information from Google using the provided ID token asynchronously.
    /// </summary>
    /// <param name="idToken">
    ///   The Google ID token to be used for fetching user information.
    /// </param>
    /// <returns>
    ///   A Task that represents the asynchronous operation. The task result contains
    ///   GoogleUserResponse with user information.
    /// </returns>
    Task<GoogleUserResponse> GetUserInformationAsync(string idToken);
}


internal sealed class GoogleAuthService : IGoogleAuthService
{
    private readonly GoogleAuthConfig _googleAuthConfig;

    public GoogleAuthService(IOptions<SocialSettings> socialSettings)
    {
        _googleAuthConfig = socialSettings.Value.GoogleAuthConfig;
    }

    public async Task<GoogleUserResponse> GetUserInformationAsync(string idToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(idToken);

        Payload payload = await ValidateAsync(idToken, new ValidationSettings
        {
            Audience = new[] { _googleAuthConfig.ClientId },
            IssuedAtClockTolerance = TimeSpan.FromSeconds(1)
        });

        GoogleUserResponse googleUserResponse = new()
        {
            FirstName = payload.GivenName,
            LastName = payload.FamilyName,
            Email = payload.Email,
            EmailVerified = payload.EmailVerified,
            ProfilePicture = payload.Picture,
            LoginProviderSubject = payload.Subject
        };

        return googleUserResponse;
    }
}
