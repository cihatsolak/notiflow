using Notiflow.Backoffice.Application.Models.Notifications;

namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class FirebaseManager : IFirebaseService
{
    private readonly IRestService _restService;
    private readonly IRedisService _redisService;

    public FirebaseManager(
        IRestService restService, 
        IRedisService redisService)
    {
        _restService = restService;
        _redisService = redisService;
    }

    public async Task<FirebaseNotificationResponse> SendNotificationAsync(FirebaseSingleNotificationRequest firebaseRequest, CancellationToken cancellationToken)
    {
        await TenantAuthorizationCheckAsync();

        var tenantApplication = await _redisService.GetAsync<TenantApplicationCacheModel>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_APPS_INFORMATION)) 
            ?? throw new TenantException("The tenant's application information could not be found.");

        var credentials = HttpClientHeaderExtensions
                           .Generate(HeaderNames.Authorization, $"key={tenantApplication.FirebaseServerKey}")
                           .AddItem("Sender", $"id={tenantApplication.FirebaseSenderId}");
    
        return await _restService.PostResponseAsync<FirebaseNotificationResponse>("firebase", "fcm/send", firebaseRequest, credentials, cancellationToken);
    }

    public async Task<FirebaseNotificationResponse> SendNotificationsAsync(FirebaseMultipleNotificationRequest firebaseRequest, CancellationToken cancellationToken)
    {
        await TenantAuthorizationCheckAsync();

        var tenantApplication = await _redisService.GetAsync<TenantApplicationCacheModel>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_APPS_INFORMATION)) 
            ?? throw new TenantException("The tenant's application information could not be found.");
        
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
            throw new TenantException("The tenant is not authorized to send notification.");
        }
    }
}
