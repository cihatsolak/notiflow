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
