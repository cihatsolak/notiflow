namespace Notiflow.Backoffice.Infrastructure.Services;

public sealed class FirebaseManager : IFirebaseService
{
    private readonly IRestService _restService;

    public FirebaseManager(IRestService restService)
    {
        _restService = restService;
    }

    public Task<FirebaseNotificationResponse> SendNotificationAsync(CancellationToken cancellationToken)
    {
        var credentials = HttpClientExtensions.GenerateHeader(HeaderNames.Authorization, $"key=xxx");
        credentials.AddHeaderItem("Sender", $"id=xxx");

        return _restService.PostResponseAsync<FirebaseNotificationResponse>("ClientName", "RouteUrl", credentials, cancellationToken);
    }
}
