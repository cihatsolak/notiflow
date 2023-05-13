namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;

public sealed class SendNotificationHandler : IRequestHandler<SendNotificationRequest, Response<Unit>>
{
    private readonly IFirebaseService _firebaseService;

    public SendNotificationHandler(IFirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
    }

    public async Task<Response<Unit>> Handle(SendNotificationRequest request, CancellationToken cancellationToken)
    {
        var firebaseNotificationResponse = await _firebaseService.SendNotificationAsync(null, cancellationToken);
        if (firebaseNotificationResponse.Succeeded)
        {
            return Response<Unit>.Fail(-1);
        }

        return Response<Unit>.Success(1);
    }
}