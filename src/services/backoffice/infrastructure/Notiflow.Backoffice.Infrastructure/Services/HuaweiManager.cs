namespace Notiflow.Backoffice.Infrastructure.Services;

public sealed class HuaweiManager : IHuaweiService
{
    private readonly IRestService _restService;
    private readonly IRedisService _redisService;
    private readonly ILogger<HuaweiManager> _logger;

    public HuaweiManager(
        IRestService restService,
        IRedisService redisService,
        ILogger<HuaweiManager> logger)
    {
        _restService = restService;
        _redisService = redisService;
        _logger = logger;
    }

    public async Task<bool> SendNotificationsAsync(HuaweiNotificationRequest request, CancellationToken cancellationToken)
    {
        var tenantApplication = await _redisService.GetAsync<TenantApplicationCacheModel>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_APPS_INFORMATION));
        if (tenantApplication is null)
        {
            _logger.LogWarning("The tenant's application information could not be found.");
            return false;
        }

        List<KeyValuePair<string, string>> credentials = new()
        {
            new KeyValuePair<string, string>("grant_type", "xxxx"),
            new KeyValuePair<string, string>("client_secret", "xxxx"),
            new KeyValuePair<string, string>("client_id", "xxxx")
        };

        var authenticationResponse = await _restService.PostEncodedResponseAsync<HuaweiAuthenticationResponse>("Huawei", "routeUrl", credentials, cancellationToken);
        if (authenticationResponse is null)
        {
            _logger.LogWarning("Failed to connect with huawei store. Failed to authenticate.");
            return default;
        }

        var auhorizationCollection = HttpClientHeaderExtensions.Generate(HeaderNames.Authorization, $"{authenticationResponse.TokenType} {authenticationResponse.AccessToken}");

        var notificationResponse = await _restService.PostResponseAsync<HuaweiNotificationResponse>("Huawei", "routeurl", request, auhorizationCollection, cancellationToken);
        if (notificationResponse is null)
        {
            return default;
        }

        if (!notificationResponse.Succeeded)
        {
            return default;
        }

        return true;
    }
}
