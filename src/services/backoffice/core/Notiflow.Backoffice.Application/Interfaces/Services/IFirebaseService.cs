namespace Notiflow.Backoffice.Application.Interfaces.Services;

public interface IFirebaseService
{
    Task<FirebasePushResponse> SendNotificationAsync(FirebaseSingleRequest firebaseRequest, CancellationToken cancellationToken);
    Task<bool> SendNotificationsAsync(FirebaseMultipleRequest firebaseRequest, CancellationToken cancellationToken);
}