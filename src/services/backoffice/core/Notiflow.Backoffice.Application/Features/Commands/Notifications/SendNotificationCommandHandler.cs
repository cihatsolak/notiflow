namespace Notiflow.Backoffice.Application.Features.Commands.Notifications;

public sealed record SendNotificationCommand(
    List<int> CustomerIds,
    string Title,
    string Message,
    string ImageUrl
    ) : IRequest<Result<Unit>>;

public sealed class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _notiflowUnitOfWork;
    private readonly IFirebaseService _firebaseService;
    private readonly IHuaweiService _huaweiService;
    private readonly IPublishEndpoint _publishEndpoint;

    public SendNotificationCommandHandler(
        INotiflowUnitOfWork notiflowUnitOfWork,
        IFirebaseService firebaseService,
        IHuaweiService huaweiService,
        IPublishEndpoint publishEndpoint)
    {
        _notiflowUnitOfWork = notiflowUnitOfWork;
        _firebaseService = firebaseService;
        _huaweiService = huaweiService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result<Unit>> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        List<Device> devices = await _notiflowUnitOfWork.DeviceRead.GetCloudMessagePlatformByCustomerIdsAsync(request.CustomerIds, cancellationToken);
        if (devices.IsNullOrNotAny())
        {
            return Result<Unit>.Status404NotFound(ResultCodes.DEVICE_NOT_FOUND);
        }

        var firesabeDeviceTokens = devices
                                  .Where(device => device.CloudMessagePlatform == CloudMessagePlatform.Firesabe)
                                  .Select(p => p.Token)
                                  .ToList();

        if (!firesabeDeviceTokens.IsNullOrNotAny())
        {
            var firebaseNotificationResult = await SendFirebaseNotifyAsync(request, firesabeDeviceTokens, cancellationToken);
            if (!firebaseNotificationResult.Succeeded)
            {
                var notificationNotDeliveredEvent = ObjectMapper.Mapper.Map<NotificationNotDeliveredEvent>(request);

                notificationNotDeliveredEvent.SenderIdentity = firebaseNotificationResult.SecretIdentity;
                notificationNotDeliveredEvent.ErrorMessage = firebaseNotificationResult.ErrorMessage;

                await _publishEndpoint.Publish(notificationNotDeliveredEvent, cancellationToken);
            }
            else
            {
                var notificationDeliveredEvent = ObjectMapper.Mapper.Map<NotificationDeliveredEvent>(request);
                notificationDeliveredEvent.SenderIdentity = firebaseNotificationResult.SecretIdentity;

                await _publishEndpoint.Publish(notificationDeliveredEvent, cancellationToken);
            }
        }

        var huaweiDeviceTokens = devices
                                 .Where(device => device.CloudMessagePlatform == CloudMessagePlatform.Huawei)
                                 .Select(p => p.Token)
                                 .ToList();

        if (!huaweiDeviceTokens.IsNullOrNotAny())
        {
            var huaweiNotificationResult = await SendHuaweiNotifyAsync(request, huaweiDeviceTokens, cancellationToken);
            if (!huaweiNotificationResult.Succeeded)
            {
                var notificationNotDeliveredEvent = ObjectMapper.Mapper.Map<NotificationNotDeliveredEvent>(request);
                notificationNotDeliveredEvent.SenderIdentity = huaweiNotificationResult.SecretIdentity;
                notificationNotDeliveredEvent.ErrorMessage = huaweiNotificationResult.ErrorMessage;

                await _publishEndpoint.Publish(notificationNotDeliveredEvent, cancellationToken);
            }
            else
            {
                var notificationDeliveredEvent = ObjectMapper.Mapper.Map<NotificationDeliveredEvent>(request);
                notificationDeliveredEvent.SenderIdentity = huaweiNotificationResult.SecretIdentity;

                await _publishEndpoint.Publish(notificationDeliveredEvent, cancellationToken);
            }
        }

        return Result<Unit>.Status200OK(ResultCodes.NOTIFICATION_SENDING_SUCCESSFUL);
    }

    private async Task<NotificationResult> SendFirebaseNotifyAsync(SendNotificationCommand request, List<string> deviceTokens, CancellationToken cancellationToken)
    {
        Guid secretIdentity = Guid.NewGuid();

        FirebaseMultipleNotificationRequest firebaseMultipleNotificationRequest = new()
        {
            DeviceTokens = deviceTokens,
            FirebaseMessage = new FirebaseMessage
            {
                ImageUrl = request.ImageUrl,
                Message = request.Message,
                Title = request.Title
            },
            Data = new
            {
                SecretIdentity = secretIdentity,
                Type = 1,
                Source = "{\"name\":\"John\",\"age\":30,\"city\":\"New York\"}"
            }
        };

        var notificationResult = await _firebaseService.SendNotificationsAsync(firebaseMultipleNotificationRequest, cancellationToken);
        notificationResult.SecretIdentity = secretIdentity;

        return notificationResult;
    }

    private async Task<NotificationResult> SendHuaweiNotifyAsync(SendNotificationCommand request, List<string> deviceTokens, CancellationToken cancellationToken)
    {
        Guid secretIdentity = Guid.NewGuid();

        HuaweiNotificationRequest huaweiNotificationRequest = new()
        {
            Message = new HuaweiMessage
            {
                DeviceTokens = deviceTokens,
                Data = new
                {
                    request.ImageUrl,
                    request.Message,
                    request.Title,
                    SecretIdentity = secretIdentity,
                    Type = 1,
                    Source = "{\"name\":\"John\",\"age\":30,\"city\":\"New York\"}"
                }
            }
        };

        var notificationResult = await _huaweiService.SendNotificationAsync(huaweiNotificationRequest, cancellationToken);
        notificationResult.SecretIdentity = secretIdentity;

        return notificationResult;
    }
}
