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

    public async Task<NotificationResult> SendNotificationAsync(FirebaseSingleNotificationRequest request, CancellationToken cancellationToken)
    {
        var tenantApplication = await _redisService.GetAsync<TenantApplicationCacheModel>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_APPS_INFORMATION)) 
            ?? throw new TenantException("The tenant's application information could not be found.");

        var credentials = HttpClientHeaderExtensions
                           .Generate(HeaderNames.Authorization, $"key={tenantApplication.FirebaseServerKey}")
                           .AddItem("Sender", $"id={tenantApplication.FirebaseSenderId}");
    
        var firebaseNotificationResponse = await _restService.PostResponseAsync<FirebaseNotificationResponse>("firebase", "fcm/send", request, credentials, cancellationToken);
        if (firebaseNotificationResponse is null)
        {
            _logger.LogInformation("Can't get response from firebase services.");
            return new NotificationResult();
        }

        return new NotificationResult
        {
            ErrorMessage = firebaseNotificationResponse.ErrorMessage,
            Succeeded = firebaseNotificationResponse.Succeeded
        };
    }

    public async Task<NotificationResult> SendNotificationsAsync(FirebaseMultipleNotificationRequest request, CancellationToken cancellationToken)
    {
        var tenantApplication = await _redisService.GetAsync<TenantApplicationCacheModel>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_APPS_INFORMATION)) 
            ?? throw new TenantException("The tenant's application information could not be found.");
        
        var credentials = HttpClientHeaderExtensions
                           .Generate(HeaderNames.Authorization, $"key={tenantApplication.FirebaseServerKey}")
                           .AddItem("Sender", $"id={tenantApplication.FirebaseSenderId}");

        var firebaseNotificationResponse = await _restService.PostResponseAsync<FirebaseNotificationResponse>("firebase", "fcm/send", request, credentials, cancellationToken);
        if (firebaseNotificationResponse is null)
        {
            _logger.LogInformation("Can't get response from firebase services.");
            return new NotificationResult();
        }

        return new NotificationResult(firebaseNotificationResponse.Succeeded, firebaseNotificationResponse.ErrorMessage);
    }
}
