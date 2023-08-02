namespace Notiflow.Backoffice.Application.Interfaces.Services
{
    public interface IHuaweiService
    {
        Task<bool> SendNotificationsAsync(HuaweiNotificationRequest request, CancellationToken cancellationToken);
    }
}
