namespace Notiflow.Backoffice.Application.Interfaces.Services;

public interface IFirebaseService
{
    Task<FirebaseNotificationResponse> SendNotificationAsync(FirebaseSingleNotificationRequest firebaseRequest, CancellationToken cancellationToken);
    Task<FirebaseNotificationResponse> SendNotificationsAsync(FirebaseMultipleNotificationRequest firebaseRequest, CancellationToken cancellationToken);
}