namespace Notiflow.Backoffice.Application.Interfaces.Services;

public interface IFirebaseService
{
    Task<FirebasePushResponse> SendNotificationAsync(FirebaseSingleRequest firebaseRequest, CancellationToken cancellationToken);
    Task<FirebasePushResponse> SendNotificationsAsync(FirebaseMultipleRequest firebaseRequest, CancellationToken cancellationToken);
}