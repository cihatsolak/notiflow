namespace Notiflow.Backoffice.Application.Interfaces.Services;

public interface IFirebaseService
{
    Task<FirebaseNotificationResponse> SendNotificationAsync(CancellationToken cancellationToken);
}