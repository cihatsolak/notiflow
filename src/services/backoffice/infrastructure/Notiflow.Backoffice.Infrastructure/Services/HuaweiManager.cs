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

        List<KeyValuePair<string, string>> credentials = new()
        {
            new KeyValuePair<string, string>("grant_type", tenantApplication.HuaweiGrandType),
            new KeyValuePair<string, string>("client_secret", tenantApplication.HuaweiClientSecret),
            new KeyValuePair<string, string>("client_id", tenantApplication.HuaweiClientId)
        };

        var authenticationResponse = await _restService.PostEncodedResponseAsync<HuaweiAuthenticationResponse>(nameof(HuaweiManager), _huaweiSetting.AuthenticationRoute, credentials, cancellationToken);
        if (authenticationResponse is null)
        {
            return notificationResult;
        }

        var auhorizationCollection = HttpClientHeaderExtensions
                                      .Generate(HeaderNames.Authorization, $"{authenticationResponse.TokenType} {authenticationResponse.AccessToken}");

        //sendUrl = sendUrl.Replace("{ClientId}", clientId); //Todo:

        var huaweiNotificationResponse = await _restService.PostResponseAsync<HuaweiNotificationResponse>(nameof(HuaweiManager), _huaweiSetting.NotificationRoute, request, auhorizationCollection, cancellationToken);
        if (huaweiNotificationResponse is null)
        {
            return notificationResult;
        }

        return new NotificationResult(huaweiNotificationResponse.Succeeded, huaweiNotificationResponse.ErrorMessage);
    }
}
