namespace Notiflow.Backoffice.Infrastructure.Services;

public sealed class FirebaseManager : IFirebaseService
{
    private readonly IRestService _restService;

    public FirebaseManager(IRestService restService)
    {
        _restService = restService;
    }

    public async Task<FirebaseResponse> SendNotificationAsync(FirebaseSingleRequest firebaseRequest, CancellationToken cancellationToken)
    {
        var credentials = HttpClientExtensions.GenerateHeader(HeaderNames.Authorization, $"key=xxx");
        credentials.AddHeaderItem("Sender", $"id=xxx");

        return await _restService.PostResponseAsync<FirebaseResponse>("firebase", "fcm/send", firebaseRequest, credentials, cancellationToken);
    }

    public async Task<FirebaseResponse> SendNotificationsAsync(FirebaseMultipleRequest firebaseRequest, CancellationToken cancellationToken)
    {
        var credentials = HttpClientExtensions.GenerateHeader(HeaderNames.Authorization, $"key=xxx");
        credentials.AddHeaderItem("Sender", $"id=xxx");


        return await _restService.PostResponseAsync<FirebaseResponse>("firebase", "fcm/send", firebaseRequest, credentials, cancellationToken);
    }
}
