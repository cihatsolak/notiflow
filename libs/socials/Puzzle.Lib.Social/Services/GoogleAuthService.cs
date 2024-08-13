namespace Puzzle.Lib.Social.Services;

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
            Audience = [_googleAuthConfig.ClientId],
            IssuedAtClockTolerance = TimeSpan.FromSeconds(1)
        }).ConfigureAwait(false);

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
