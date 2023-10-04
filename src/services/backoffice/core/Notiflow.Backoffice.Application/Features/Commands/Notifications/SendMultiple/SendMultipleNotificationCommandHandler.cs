namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.SendMultiple;

public sealed class SendMultipleNotificationCommandHandler : IRequestHandler<SendMultipleNotificationCommand, ApiResponse<Unit>>
{
    private readonly INotiflowUnitOfWork _notiflowUnitOfWork;
    private readonly IFirebaseService _firebaseService;
    private readonly IHuaweiService _huaweiService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<SendMultipleNotificationCommandHandler> _logger;

    public SendMultipleNotificationCommandHandler(
        INotiflowUnitOfWork notiflowUnitOfWork,
        IFirebaseService firebaseService,
        IHuaweiService huaweiService,
        IPublishEndpoint publishEndpoint,
        ILogger<SendMultipleNotificationCommandHandler> logger)
    {
        _notiflowUnitOfWork = notiflowUnitOfWork;
        _firebaseService = firebaseService;
        _huaweiService = huaweiService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<ApiResponse<Unit>> Handle(SendMultipleNotificationCommand request, CancellationToken cancellationToken)
    {
        List<Device> devices = await _notiflowUnitOfWork.DeviceRead.GetCloudMessagePlatformByCustomerIdsAsync(request.CustomerIds, cancellationToken);
        if (devices.IsNullOrNotAny())
        {
            return ApiResponse<Unit>.Fail(ResponseCodes.Error.DEVICE_NOT_FOUND);
        }

        var firesabeDeviceTokens = devices
                                   .Where(device => device.CloudMessagePlatform == CloudMessagePlatform.Firesabe)
                                   .Select(p => p.Token)
                                   .ToList();

        if (!firesabeDeviceTokens.IsNullOrNotAny())
        {
            var firebaseNotificationResult = await SendNotifyWithFirebase(request, firesabeDeviceTokens, cancellationToken);
            if (!firebaseNotificationResult.Succeeded)
            {
                var notificationNotDeliveredEvent = ObjectMapper.Mapper.Map<NotificationNotDeliveredEvent>(request);
                ObjectMapper.Mapper.Map(firebaseNotificationResult, notificationNotDeliveredEvent);

                await _publishEndpoint.Publish(notificationNotDeliveredEvent, cancellationToken);

                _logger.LogWarning("Sending multiple notifications with firebase failed.");
            }
            else
            {
                var notificationDeliveredEvent = ObjectMapper.Mapper.Map<NotificationDeliveredEvent>(request);
                ObjectMapper.Mapper.Map(firebaseNotificationResult, notificationDeliveredEvent);

                await _publishEndpoint.Publish(notificationDeliveredEvent, cancellationToken);

                _logger.LogInformation("Sending multiple notifications with firebase is successful.");
            }
        }

        var huaweiDeviceTokens = devices
                                 .Where(device => device.CloudMessagePlatform == CloudMessagePlatform.Huawei)
                                 .Select(p => p.Token)
                                 .ToList();

        if (huaweiDeviceTokens.IsNullOrNotAny())
        {
            var huaweiNotificationResult = await SendNotifyWithHuawei(request, huaweiDeviceTokens, cancellationToken);
            if (!huaweiNotificationResult.Succeeded)
            {
                var notificationNotDeliveredEvent = ObjectMapper.Mapper.Map<NotificationNotDeliveredEvent>(request);
                ObjectMapper.Mapper.Map(huaweiNotificationResult, notificationNotDeliveredEvent);

                await _publishEndpoint.Publish(notificationNotDeliveredEvent, cancellationToken);

                _logger.LogWarning("Sending multiple notifications with huawei failed.");
            }
            else
            {
                var notificationDeliveredEvent = ObjectMapper.Mapper.Map<NotificationDeliveredEvent>(request);
                ObjectMapper.Mapper.Map(huaweiNotificationResult, notificationDeliveredEvent);

                await _publishEndpoint.Publish(notificationDeliveredEvent, cancellationToken);

                _logger.LogInformation("Sending multiple notifications with huawei is successful.");
            }
        }

        return ApiResponse<Unit>.Success(ResponseCodes.Success.NOTIFICATION_SENDING_SUCCESSFUL);
    }

    private async Task<NotificationResult> SendNotifyWithFirebase(SendMultipleNotificationCommand request, List<string> deviceTokens, CancellationToken cancellationToken)
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

    private async Task<NotificationResult> SendNotifyWithHuawei(SendMultipleNotificationCommand request, List<string> deviceTokens, CancellationToken cancellationToken)
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
