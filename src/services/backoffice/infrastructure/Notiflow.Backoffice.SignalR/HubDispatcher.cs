namespace Notiflow.Backoffice.SignalR;

public interface IHubDispatcher
{
}

public sealed class HubDispatcher : IHubDispatcher
{
    private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;

    public HubDispatcher(IHubContext<NotificationHub, INotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendExampleAsync(string message)
    {
        await _hubContext.Clients.All.ReceiveExample(message);
    }
}