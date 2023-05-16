namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.SendMultiple;

public sealed class SendMultipleNotificationCommandHandler : IRequestHandler<SendMultipleNotificationCommand, Response<Unit>>
{
    private readonly IFirebaseService _firebaseService;
    private readonly ILogger<SendMultipleNotificationCommandHandler> _logger;

    public SendMultipleNotificationCommandHandler(
        IFirebaseService firebaseService, 
        ILogger<SendMultipleNotificationCommandHandler> logger)
    {
        _firebaseService = firebaseService;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(SendMultipleNotificationCommand request, CancellationToken cancellationToken)
    {
        var firebaseNotificationResponse = await _firebaseService.SendNotificationAsync(null, cancellationToken);
        if (firebaseNotificationResponse.Succeeded)
        {
            _logger.LogWarning("");
            return Response<Unit>.Fail(-1);
        }

        return Response<Unit>.Success(1);
    }
}
