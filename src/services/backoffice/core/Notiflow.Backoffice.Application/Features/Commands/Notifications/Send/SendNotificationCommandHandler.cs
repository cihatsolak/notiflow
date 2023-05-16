namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;

public sealed class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, Response<Unit>>
{
    private readonly IFirebaseService _firebaseService;

    public SendNotificationCommandHandler(IFirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
    }

    public async Task<Response<Unit>> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var firebaseNotificationResponse = await _firebaseService.SendNotificationAsync(null, cancellationToken);
        if (firebaseNotificationResponse.Succeeded)
        {
            return Response<Unit>.Fail(-1);
        }

        return Response<Unit>.Success(1);
    }
}