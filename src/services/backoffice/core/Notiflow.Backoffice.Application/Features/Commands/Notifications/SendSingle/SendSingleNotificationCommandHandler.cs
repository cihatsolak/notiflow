namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.SendSingle;

public sealed class SendSingleNotificationCommandHandler : IRequestHandler<SendSingleNotificationCommand, ApiResponse<Unit>>
{
    private readonly INotiflowUnitOfWork _notiflowUnitOfWork;
    private readonly IFirebaseService _firebaseService;
    private readonly IHuaweiService _huaweiService;
    private readonly IPublishEndpoint _publishEndpoint;

    public SendSingleNotificationCommandHandler(
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

    public async Task<ApiResponse<Unit>> Handle(SendSingleNotificationCommand request, CancellationToken cancellationToken)
    {
        Device device = await _notiflowUnitOfWork.DeviceRead.GetCloudMessagePlatformByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (device is null)
        {
            return ApiResponse<Unit>.Fail(ResponseCodes.Error.DEVICE_NOT_FOUND);
        }

        var notificationResult = device.CloudMessagePlatform switch
        {
            CloudMessagePlatform.Firesabe => await SendFirebaseNotifyAsync(request, device.Token, cancellationToken),
            CloudMessagePlatform.Huawei => await SendHuaweiNotifyAsync(request, device.Token, cancellationToken),
            _ => throw new DeviceException("The cloud messaging platform could not be determined."),
        };

        if (!notificationResult.Succeeded)
        {
            var notificationNotDeliveredEvent = ObjectMapper.Mapper.Map<NotificationNotDeliveredEvent>(request);
            notificationNotDeliveredEvent.SenderIdentity = notificationResult.SecretIdentity;
            notificationNotDeliveredEvent.ErrorMessage = notificationResult.ErrorMessage;

            await _publishEndpoint.Publish(notificationNotDeliveredEvent, cancellationToken);
            
            return ApiResponse<Unit>.Fail(ResponseCodes.Error.NOTIFICATION_SENDING_FAILED);
        }

        var notificationDeliveredEvent = ObjectMapper.Mapper.Map<NotificationDeliveredEvent>(request);
        notificationDeliveredEvent.SenderIdentity = notificationResult.SecretIdentity;

        await _publishEndpoint.Publish(notificationDeliveredEvent, cancellationToken);

        return ApiResponse<Unit>.Success(ResponseCodes.Success.NOTIFICATION_SENDING_SUCCESSFUL);
    }

    private async Task<NotificationResult> SendFirebaseNotifyAsync(SendSingleNotificationCommand request, string deviceToken, CancellationToken cancellationToken)
    {
        Guid secretIdentity = Guid.NewGuid();

        FirebaseSingleNotificationRequest singleNotificationRequest = new()
        {
            DeviceToken = deviceToken,
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

        var notificationResult = await _firebaseService.SendNotificationAsync(singleNotificationRequest, cancellationToken);
        notificationResult.SecretIdentity = secretIdentity;

        return notificationResult;
    }

    private async Task<NotificationResult> SendHuaweiNotifyAsync(SendSingleNotificationCommand request, string deviceToken, CancellationToken cancellationToken)
    {
        Guid secretIdentity = Guid.NewGuid();

        HuaweiNotificationRequest notificationRequest = new()
        {
            Message = new HuaweiMessage
            {
                DeviceTokens = new List<string>
                {
                    deviceToken
                },
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

        var notificationResult = await _huaweiService.SendNotificationAsync(notificationRequest, cancellationToken);
        notificationResult.SecretIdentity = secretIdentity;

        return notificationResult;
    }
}