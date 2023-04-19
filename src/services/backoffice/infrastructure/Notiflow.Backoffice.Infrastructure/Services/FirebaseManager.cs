namespace Notiflow.Backoffice.Infrastructure.Services;

public sealed class FirebaseManager : IFirebaseService
{
    private readonly IRestService _restService;

    public FirebaseManager(IRestService restService)
    {
        _restService = restService;
    }

    public async Task<FirebasePushResponse> SendNotificationAsync(FirebaseSingleRequest firebaseRequest, CancellationToken cancellationToken)
    {
        var credentials = HttpClientExtensions
                            .GenerateHeader(HeaderNames.Authorization, $"key=xxx")
                            .AddHeaderItem("Sender", $"id=xxx");
    
        return await _restService.PostResponseAsync<FirebasePushResponse>("firebase", "fcm/send", firebaseRequest, credentials, cancellationToken);
    }

    public async Task<FirebasePushResponse> SendNotificationsAsync(FirebaseMultipleRequest firebaseRequest, CancellationToken cancellationToken)
    {
        var credentials = HttpClientExtensions
                           .GenerateHeader(HeaderNames.Authorization, $"key=xxx")
                           .AddHeaderItem("Sender", $"id=xxx");

        return await _restService.PostResponseAsync<FirebasePushResponse>("firebase", "fcm/send", firebaseRequest, credentials, cancellationToken);
    }
}
