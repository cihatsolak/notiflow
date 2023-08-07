namespace Notiflow.Backoffice.Application.Interfaces.Services;

public interface IHuaweiService
{
    Task<NotificationResult> SendNotificationAsync(HuaweiNotificationRequest request, CancellationToken cancellationToken);
}
