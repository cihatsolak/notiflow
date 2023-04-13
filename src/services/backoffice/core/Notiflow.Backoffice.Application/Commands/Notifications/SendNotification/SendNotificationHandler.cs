namespace Notiflow.Backoffice.Application.Commands.Notifications.SendNotification;

public sealed class SendNotificationHandler : IRequestHandler<SendNotificationRequest, ResponseModel<Unit>>
{
    private readonly IFirebaseService _firebaseService;

    public SendNotificationHandler(IFirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
    }

    public async Task<ResponseModel<Unit>> Handle(SendNotificationRequest request, CancellationToken cancellationToken)
    {
        var firebaseNotificationResponse = await _firebaseService.SendNotificationAsync(cancellationToken);
        if (firebaseNotificationResponse.IsSuccess)
        {
            return ResponseModel<Unit>.Fail(-1);
        }

        return ResponseModel<Unit>.Success(1);
    }
}