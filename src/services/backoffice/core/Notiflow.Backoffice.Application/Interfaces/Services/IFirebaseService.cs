namespace Notiflow.Backoffice.Application.Interfaces.Services;

public interface IFirebaseService
{
    Task<FirebaseResponse> SendNotificationAsync(FirebaseSingleRequest firebaseRequest, CancellationToken cancellationToken);
    Task<FirebaseResponse> SendNotificationsAsync(FirebaseMultipleRequest firebaseRequest, CancellationToken cancellationToken);
}