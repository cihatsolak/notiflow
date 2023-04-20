namespace Notiflow.Backoffice.Infrastructure.Services;

public sealed class HuaweiManager : IHuaweiService
{
    private readonly IRestService _restService;
    private readonly ILogger<HuaweiManager> _logger;

    public HuaweiManager(IRestService restService, ILogger<HuaweiManager> logger)
    {
        _restService = restService;
        _logger = logger;
    }

    public async Task<bool> SendNotificationsAsync(FirebaseMultipleRequest firebaseRequest, CancellationToken cancellationToken)
    {
        var credentials = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("grant_type", "xxxx"),
            new KeyValuePair<string, string>("client_secret", "xxxx"),
            new KeyValuePair<string, string>("client_id", "xxxx")
        };

        var huaweiAuthenticationResponse = await _restService.PostEncodedResponseAsync<HuaweiAuthenticationResponse>("Huawei", "routeUrl", credentials, cancellationToken);
        if (huaweiAuthenticationResponse is null)
        {
            _logger.LogWarning("Failed to connect with huawei store. Failed to authenticate.");
            return default;
        }

        HuaweiPushRequest huaweiPushRequest = new()
        {
            Message = new()
            {
                DeviceTokens = new()
                {
                    "devicetoken"
                },
                Data = new()
            }
        };

        var auhorization = HttpClientExtensions.GenerateHeader(HeaderNames.Authorization, $"{huaweiAuthenticationResponse.TokenType} {huaweiAuthenticationResponse.AccessToken}");

        var huaweiPushResponse = await _restService.PostResponseAsync<HuaweiPushResponse>("Huawei", "routeurl", huaweiPushRequest, auhorization, cancellationToken);
        if (huaweiPushResponse is null)
        {
            return default;
        }

        if (huaweiPushResponse.Code.Equals("") || !huaweiPushResponse.ErrorMessage.Equals("", StringComparison.OrdinalIgnoreCase))
        {
            return default;
        }

        return true;
    }
}
