namespace Puzzle.Lib.Social.Services;

/// <summary>
///   Interface for Facebook authentication service.
/// </summary>
public interface IFacebookAuthService
{
    /// <summary>
    ///   Validates a Facebook access token asynchronously.
    /// </summary>
    /// <param name="accessToken">The Facebook access token to be validated.</param>
    /// <param name="cancellationToken">(Optional) A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    ///   A Task that represents the asynchronous operation. The task result contains
    ///   FacebookTokenValidationData when validating a token.
    /// </returns>
    Task<FacebookTokenValidationData> ValidateTokenAsync(string accessToken, CancellationToken cancellationToken);

    /// <summary>
    ///   Retrieves user information from Facebook asynchronously.
    /// </summary>
    /// <param name="accessToken">The Facebook access token to be used for fetching user information.</param>
    /// <param name="cancellationToken">(Optional) A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    ///   A Task that represents the asynchronous operation. The task result contains
    ///   FacebookUserInfoResponse when fetching user information.
    /// </returns>
    Task<FacebookUserInfoResponse> GetUserInformationAsync(string accessToken, CancellationToken cancellationToken);
}
