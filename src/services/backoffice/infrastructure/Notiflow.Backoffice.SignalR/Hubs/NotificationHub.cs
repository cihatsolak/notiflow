namespace Notiflow.Backoffice.SignalR.Hubs;

public interface INotificationHub
{
    Task ReceiveExample(string message);
}

public sealed class NotificationHub(
    IRedisService redisService,
    ILogger<NotificationHub> logger) : Hub<INotificationHub>
{
    public override async Task OnConnectedAsync()
    {
        await redisService.HashSetAsync(HubExtensions.HUB_USERS, Context.User.Id(), Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        if (exception is null)
        {
            await redisService.HashDeleteAsync(HubExtensions.HUB_USERS, Context.User.Id());
        }
        else
        {
            logger.LogError(exception, "An error occurred while closing the hub connection.");
        }

        await base.OnDisconnectedAsync(exception);
    }
}
