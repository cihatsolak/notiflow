namespace Puzzle.Lib.Social.Services;

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


    public async Task<FacebookTokenValidationData> ValidateTokenAsync(string accessToken, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(accessToken);

        var facebookTokenValidationUri = string.Format(_facebookAuthConfig.TokenValidationUrl, accessToken, _facebookAuthConfig.AppId, _facebookAuthConfig.AppSecret);
        var httpResponseMessage = await _httpClient.GetAsync(facebookTokenValidationUri, cancellationToken);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation("Failed to verify facebook access token. Access token: {accessToken}", accessToken);
            return default;
        }

        var facebookTokenValidationResponse = await httpResponseMessage.Content.ReadFromJsonAsync<FacebookTokenValidationResponse>(cancellationToken: cancellationToken);
        return facebookTokenValidationResponse.Data;
    }

    public async Task<FacebookUserInfoResponse> GetUserInformationAsync(string accessToken, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(accessToken);

        string facebookUserInfoUri = string.Format(_facebookAuthConfig.UserInfoUrl, accessToken);
        var httpResponseMessage = await _httpClient.GetAsync(facebookUserInfoUri, cancellationToken);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation("Facebook user information could not be accessed. Access token: {accessToken}", accessToken);
            return default;
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<FacebookUserInfoResponse>(cancellationToken: cancellationToken);
    }
}
