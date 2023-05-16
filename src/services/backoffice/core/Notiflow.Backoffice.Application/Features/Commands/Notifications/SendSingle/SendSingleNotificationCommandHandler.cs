namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;

public sealed class SendSingleNotificationCommandHandler : IRequestHandler<SendSingleNotificationCommand, Response<Unit>>
{
    private readonly IFirebaseService _firebaseService;
    private readonly ILogger<SendSingleNotificationCommandHandler> _logger;

    public SendSingleNotificationCommandHandler(
        IFirebaseService firebaseService, 
        ILogger<SendSingleNotificationCommandHandler> logger)
    {
        _firebaseService = firebaseService;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(SendSingleNotificationCommand request, CancellationToken cancellationToken)
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