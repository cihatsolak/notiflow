namespace Notiflow.Backoffice.Application.Interfaces.Services;

public interface IFirebaseService
{
    Task<NotificationResult> SendNotificationAsync(FirebaseSingleNotificationRequest request, CancellationToken cancellationToken);
    Task<NotificationResult> SendNotificationsAsync(FirebaseMultipleNotificationRequest request, CancellationToken cancellationToken);
}