namespace Notiflow.Backoffice.Application.Interfaces.Services;

public interface IHuaweiService
{
    Task<HuaweiNotificationResponse> SendNotificationAsync(HuaweiNotificationRequest request, CancellationToken cancellationToken);
}
