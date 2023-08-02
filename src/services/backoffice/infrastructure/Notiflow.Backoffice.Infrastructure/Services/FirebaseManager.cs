namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class FirebaseManager : IFirebaseService
{
    private readonly IRestService _restService;
    private readonly IRedisService _redisService;
    private readonly ILogger<FirebaseManager> _logger;

    public FirebaseManager(
        IRestService restService, 
        IRedisService redisService,
        ILogger<FirebaseManager> logger)
    {
        _restService = restService;
        _redisService = redisService;
        _logger = logger;
    }

    public async Task<FirebaseNotificationResponse> SendNotificationAsync(FirebaseSingleNotificationRequest firebaseRequest, CancellationToken cancellationToken)
    {
        await TenantAuthorizationCheckAsync();

        var tenantApplication = await _redisService.GetAsync<TenantApplicationCacheModel>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_APPS_INFORMATION));
        if (tenantApplication is null)
        {
            _logger.LogWarning("The tenant's application information could not be found.");
            throw new Exception("");
        }

        var credentials = HttpClientHeaderExtensions
                           .Generate(HeaderNames.Authorization, $"key={tenantApplication.FirebaseServerKey}")
                           .AddItem("Sender", $"id={tenantApplication.FirebaseSenderId}");
    
        return await _restService.PostResponseAsync<FirebaseNotificationResponse>("firebase", "fcm/send", firebaseRequest, credentials, cancellationToken);
    }

    public async Task<FirebaseNotificationResponse> SendNotificationsAsync(FirebaseMultipleNotificationRequest firebaseRequest, CancellationToken cancellationToken)
    {
        await TenantAuthorizationCheckAsync();

        var tenantApplication = await _redisService.GetAsync<TenantApplicationCacheModel>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_APPS_INFORMATION));
        if (tenantApplication is null)
        {
            _logger.LogWarning("The tenant's application information could not be found.");
            throw new Exception("");
        }

        var credentials = HttpClientHeaderExtensions
                           .Generate(HeaderNames.Authorization, $"key={tenantApplication.FirebaseServerKey}")
                           .AddItem("Sender", $"id={tenantApplication.FirebaseSenderId}");

    
        return await _restService.PostResponseAsync<FirebaseNotificationResponse>("firebase", "fcm/send", firebaseRequest, credentials, cancellationToken);
    }

    private async Task TenantAuthorizationCheckAsync()
    {
        bool isSentNotificationAllowed = await _redisService.HashGetAsync<bool>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_PERMISSION), CacheKeys.NOTIFICATION_PERMISSION);
        if (!isSentNotificationAllowed)
        {
            _logger.LogWarning("The tenant is not authorized to send notification.");
            throw new Exception("");
        }
    }
}
