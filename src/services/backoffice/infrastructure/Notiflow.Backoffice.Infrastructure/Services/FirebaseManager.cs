namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class FirebaseManager : IFirebaseService
{
    private readonly IRestService _restService;
    private readonly ILogger<FirebaseManager> _logger;

    public FirebaseManager(IRestService restService, ILogger<FirebaseManager> logger)
    {
        _restService = restService;
        _logger = logger;
    }

    public async Task<FirebasePushResponse> SendNotificationAsync(FirebaseSingleRequest firebaseRequest, CancellationToken cancellationToken)
    {
        var credentials = HttpClientExtensions
                            .GenerateHeader(HeaderNames.Authorization, $"key=xxx")
                            .AddHeaderItem("Sender", $"id=xxx");
    
        return await _restService.PostResponseAsync<FirebasePushResponse>("firebase", "fcm/send", firebaseRequest, credentials, cancellationToken);
    }

    public async Task<bool> SendNotificationsAsync(FirebaseMultipleRequest firebaseRequest, CancellationToken cancellationToken)
    {
        var credentials = HttpClientExtensions
                           .GenerateHeader(HeaderNames.Authorization, $"key=xxx")
                           .AddHeaderItem("Sender", $"id=xxx");

        var firebasePushResponse = await _restService.PostResponseAsync<FirebasePushResponse>("firebase", "fcm/send", firebaseRequest, credentials, cancellationToken);
        if (!firebasePushResponse.Succeeded)
        {
            _logger.LogWarning("firebase notification sending failed.");
        }

        return firebasePushResponse.Succeeded;
    }
}
