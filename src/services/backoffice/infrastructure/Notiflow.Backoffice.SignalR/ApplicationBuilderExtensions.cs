namespace Notiflow.Backoffice.SignalR;

public static class ApplicationBuilderExtensions
{
    public static void MapHubs(this WebApplication webApplication)
    {
        webApplication.MapHub<NotificationHub>("/notification-hub");
    }
}
