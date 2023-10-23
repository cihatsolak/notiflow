using Notiflow.Common.Exceptions;

namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class FirebaseManager : IFirebaseService
{
    internal static readonly NotificationResult notificationResult = new();

    private readonly IRestService _restService;
    private readonly IRedisService _redisService;
    private readonly FirebaseSetting _firebaseSetting;

    public FirebaseManager(
        IRestService restService,
        IRedisService redisService,
        IOptions<FirebaseSetting> firebaseSetting)
    {
        _restService = restService;
        _redisService = redisService;
        _firebaseSetting = firebaseSetting.Value;
    }

    public async Task<NotificationResult> SendNotificationAsync(FirebaseSingleNotificationRequest request, CancellationToken cancellationToken)
    {
        var tenantApplication = await _redisService.HashGetAsync<TenantApplicationCacheModel>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_APPS_CONFIG)
            ?? throw new TenantException("The tenant's application information could not be found.");

        var credentials = HttpClientHeaderExtensions
                           .Generate(HeaderNames.Authorization, $"key={tenantApplication.FirebaseServerKey}")
                           .AddItem("Sender", $"id={tenantApplication.FirebaseSenderId}");

        var firebaseNotificationResponse = await _restService.PostResponseAsync<FirebaseNotificationResponse>(nameof(FirebaseManager), _firebaseSetting.Route, request, credentials, cancellationToken);
        if (firebaseNotificationResponse is null)
        {
            return notificationResult;
        }

        return new NotificationResult(firebaseNotificationResponse.Succeeded, firebaseNotificationResponse.ErrorMessage);
    }

    public async Task<NotificationResult> SendNotificationsAsync(FirebaseMultipleNotificationRequest request, CancellationToken cancellationToken)
    {
        var tenantApplication = await _redisService.HashGetAsync<TenantApplicationCacheModel>(CacheKeys.TENANT_INFO, CacheKeys.TENANT_APPS_CONFIG)
            ?? throw new TenantException("The tenant's application information could not be found.");

        var credentials = HttpClientHeaderExtensions
                           .Generate(HeaderNames.Authorization, $"key={tenantApplication.FirebaseServerKey}")
                           .AddItem("Sender", $"id={tenantApplication.FirebaseSenderId}");

        var firebaseNotificationResponse = await _restService.PostResponseAsync<FirebaseNotificationResponse>(nameof(FirebaseManager), _firebaseSetting.Route, request, credentials, cancellationToken);
        if (firebaseNotificationResponse is null)
        {
            return notificationResult;
        }

        return new NotificationResult(firebaseNotificationResponse.Succeeded, firebaseNotificationResponse.ErrorMessage);
    }
}
