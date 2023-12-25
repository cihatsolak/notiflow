namespace Notiflow.Backoffice.SignalR.Hubs;

public interface INotificationHub
{
    Task ReceiveExample(string message);
}

public sealed class NotificationHub : Hub<INotificationHub>
{
    private readonly IRedisService _redisService;
    private readonly ILogger<NotificationHub> _logger;

    public override async Task OnConnectedAsync()
    {
        await _redisService.HashSetAsync(HubExtensions.HUB_USERS, Context.User.Id(), Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        if (exception is null)
        {
            await _redisService.HashDeleteAsync(HubExtensions.HUB_USERS, Context.User.Id());
        }
        else
        {
            _logger.LogError(exception, "An error occurred while closing the hub connection.");
        }

        await base.OnDisconnectedAsync(exception);
    }


}
