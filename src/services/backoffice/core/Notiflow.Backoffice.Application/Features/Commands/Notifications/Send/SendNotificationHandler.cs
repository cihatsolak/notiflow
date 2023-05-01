namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;

public sealed class SendNotificationHandler : IRequestHandler<SendNotificationRequest, ResponseData<Unit>>
{
    private readonly IFirebaseService _firebaseService;

    public SendNotificationHandler(IFirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
    }

    public async Task<ResponseData<Unit>> Handle(SendNotificationRequest request, CancellationToken cancellationToken)
    {
        var firebaseNotificationResponse = await _firebaseService.SendNotificationAsync(null, cancellationToken);
        if (firebaseNotificationResponse.Succeeded)
        {
            return ResponseData<Unit>.Fail(-1);
        }

        return ResponseData<Unit>.Success(1);
    }
}