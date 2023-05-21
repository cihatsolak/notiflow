namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.SendSingle;

public sealed class SendSingleNotificationCommandHandler : IRequestHandler<SendSingleNotificationCommand, Response<Unit>>
{
    private readonly IFirebaseService _firebaseService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<SendSingleNotificationCommandHandler> _logger;

    public SendSingleNotificationCommandHandler(
        IFirebaseService firebaseService,
        IPublishEndpoint publishEndpoint,
        ILogger<SendSingleNotificationCommandHandler> logger)
    {
        _firebaseService = firebaseService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(SendSingleNotificationCommand request, CancellationToken cancellationToken)
    {
        var firebaseNotificationResponse = await _firebaseService.SendNotificationAsync(null, cancellationToken);
        if (firebaseNotificationResponse.Succeeded)
        {
            await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<NotificationNotDeliveredEvent>(request), cancellationToken);
            _logger.LogWarning("");
            return Response<Unit>.Fail(-1);
        }

        await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<NotificationDeliveredEvent>(request), cancellationToken);

        return Response<Unit>.Success(1);
    }
}