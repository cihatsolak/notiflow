namespace Notiflow.Backoffice.SignalR;

public interface IHubDispatcher
{
}

public sealed class HubDispatcher(
    IHubContext<NotificationHub, 
    INotificationHub> hubContext) : IHubDispatcher
{
    public async Task SendExampleAsync(string message)
    {
        await hubContext.Clients.All.ReceiveExample(message);
    }
}