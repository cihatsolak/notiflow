using Puzzle.Lib.Http.Infrastructure.Extensions;

namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class HuaweiManager : IHuaweiService
{
    internal static readonly NotificationResult notificationResult = new();

    private readonly IRestService _restService;
    private readonly IRedisService _redisService;
    private readonly HuaweiSetting _huaweiSetting;

    public HuaweiManager(
        IRestService restService,
        IRedisService redisService,
        IOptions<HuaweiSetting> huaweiSetting)
    {
        _restService = restService;
        _redisService = redisService;
        _huaweiSetting = huaweiSetting.Value;
    }

    public async Task<NotificationResult> SendNotificationAsync(HuaweiNotificationRequest request, CancellationToken cancellationToken)
    {
        var tenantApplication = await _redisService.HashGetAsync<TenantApplicationCacheModel>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_APPS_CONFIG)
            ?? throw new TenantException("The tenant's application information could not be found.");

        List<KeyValuePair<string, string>> credentials =
        [
            new KeyValuePair<string, string>("grant_type", tenantApplication.HuaweiGrandType),
            new KeyValuePair<string, string>("client_secret", tenantApplication.HuaweiClientSecret),
            new KeyValuePair<string, string>("client_id", tenantApplication.HuaweiClientId)
        ];

        var authenticationResponse = await _restService.PostEncodedResponseAsync<HuaweiAuthenticationResponse>(nameof(HuaweiManager), _huaweiSetting.AuthTokenServiceUrl, credentials, cancellationToken);
        if (string.IsNullOrEmpty(authenticationResponse?.AccessToken))
        {
            return new NotificationResult(authenticationResponse?.ErrorMessage);
        }

        var auhorizationCollection = NameValueCollectionExtensions
                                      .Generate(HeaderNames.Authorization, $"{authenticationResponse.TokenType} {authenticationResponse.AccessToken}");

        string sendServiceUrl = _huaweiSetting.SendServiceUrl.Replace("{ClientId}", tenantApplication.HuaweiClientId);

        var huaweiNotificationResponse = await _restService.PostResponseAsync<HuaweiNotificationResponse>(nameof(HuaweiManager), sendServiceUrl, request, auhorizationCollection, cancellationToken);
        if (huaweiNotificationResponse is null)
        {
            return notificationResult;
        }

        return new NotificationResult(huaweiNotificationResponse.Succeeded, huaweiNotificationResponse.ErrorMessage);
    }
}
