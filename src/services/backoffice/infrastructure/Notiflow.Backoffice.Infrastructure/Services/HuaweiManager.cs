namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class HuaweiManager : IHuaweiService
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

    public async Task<HuaweiNotificationResponse> SendNotificationAsync(HuaweiNotificationRequest request, CancellationToken cancellationToken)
    {
        bool isSentNotificationAllowed = await _redisService.HashGetAsync<bool>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_PERMISSION), CacheKeys.NOTIFICATION_PERMISSION);
        if (!isSentNotificationAllowed)
        {
            throw new TenantException("The tenant is not authorized to send notification.");
        }

        var tenantApplication = await _redisService.GetAsync<TenantApplicationCacheModel>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_APPS_INFORMATION)) 
            ?? throw new TenantException("The tenant's application information could not be found.");

        List<KeyValuePair<string, string>> credentials = new()
        {
            new KeyValuePair<string, string>("grant_type", tenantApplication.HuaweiGrandType),
            new KeyValuePair<string, string>("client_secret", tenantApplication.HuaweiClientSecret),
            new KeyValuePair<string, string>("client_id", tenantApplication.HuaweiClientId)
        };

        var authenticationResponse = await _restService.PostEncodedResponseAsync<HuaweiAuthenticationResponse>("Huawei", "routeUrl", credentials, cancellationToken);
        if (authenticationResponse is null)
        {
            _logger.LogError("Failed to connect with huawei store. Failed to authenticate.");
            return default;
        }

        var auhorizationCollection = HttpClientHeaderExtensions
                                      .Generate(HeaderNames.Authorization, $"{authenticationResponse.TokenType} {authenticationResponse.AccessToken}");

        //sendUrl = sendUrl.Replace("{ClientId}", clientId); //Todo:

        return await _restService.PostResponseAsync<HuaweiNotificationResponse>("Huawei", "routeurl", request, auhorizationCollection, cancellationToken);
    }
}
