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
    Task<FacebookTokenValidationData> ValidateTokenAsync(string accessToken, CancellationToken cancellationToken = default);

    /// <summary>
    ///   Retrieves user information from Facebook asynchronously.
    /// </summary>
    /// <param name="accessToken">The Facebook access token to be used for fetching user information.</param>
    /// <param name="cancellationToken">(Optional) A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    ///   A Task that represents the asynchronous operation. The task result contains
    ///   FacebookUserInfoResponse when fetching user information.
    /// </returns>
    Task<FacebookUserInfoResponse> GetUserInformationAsync(string accessToken, CancellationToken cancellationToken = default);
}


internal sealed class FacebookAuthService : IFacebookAuthService
{
    private readonly HttpClient _httpClient;
    private readonly FacebookAuthConfig _facebookAuthConfig;
    private readonly ILogger<FacebookAuthService> _logger;

    public FacebookAuthService(
        IHttpClientFactory httpClientFactory,
        IOptions<SocialSettings> socialSettings,
        ILogger<FacebookAuthService> logger)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(FacebookAuthService));
        _facebookAuthConfig = socialSettings.Value.FacebookAuthConfig;
        _logger = logger;
    }


    public async Task<FacebookTokenValidationData> ValidateTokenAsync(string accessToken, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(accessToken);

        var facebookTokenValidationUri = string.Format(_facebookAuthConfig.TokenValidationUrl, accessToken, _facebookAuthConfig.AppId, _facebookAuthConfig.AppSecret);
        var httpResponseMessage = await _httpClient.GetAsync(facebookTokenValidationUri, cancellationToken);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation("Failed to verify facebook access token. Access token: {@}", accessToken);
            return default;
        }

        var facebookTokenValidationResponse = await httpResponseMessage.Content.ReadFromJsonAsync<FacebookTokenValidationResponse>(cancellationToken: cancellationToken);
        return facebookTokenValidationResponse.Data;
    }

    public async Task<FacebookUserInfoResponse> GetUserInformationAsync(string accessToken, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(accessToken);

        string facebookUserInfoUri = string.Format(_facebookAuthConfig.UserInfoUrl, accessToken);
        var httpResponseMessage = await _httpClient.GetAsync(facebookUserInfoUri, cancellationToken);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation("Facebook user information could not be accessed. Access token: {@accessToken}", accessToken);
            return default;
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<FacebookUserInfoResponse>(cancellationToken: cancellationToken);
    }
}
