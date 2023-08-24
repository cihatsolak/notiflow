namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.SendSingle;

public sealed class SendSingleNotificationCommandHandler : IRequestHandler<SendSingleNotificationCommand, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _notiflowUnitOfWork;
    private readonly IRedisService _redisService;
    private readonly IFirebaseService _firebaseService;
    private readonly IHuaweiService _huaweiService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<SendSingleNotificationCommandHandler> _logger;

    public SendSingleNotificationCommandHandler(
        INotiflowUnitOfWork notiflowUnitOfWork,
        IRedisService redisService,
        IFirebaseService firebaseService,
        IHuaweiService huaweiService,
        IPublishEndpoint publishEndpoint,
        ILogger<SendSingleNotificationCommandHandler> logger)
    {
        _notiflowUnitOfWork = notiflowUnitOfWork;
        _redisService = redisService;
        _firebaseService = firebaseService;
        _huaweiService = huaweiService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(SendSingleNotificationCommand request, CancellationToken cancellationToken)
    {
        bool isSentNotificationAllowed = await _redisService.HashGetAsync<bool>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_NOTIFICATION_PERMISSION);
        if (!isSentNotificationAllowed)
        {
            _logger.LogWarning("The tenant is not authorized to send notification.");
            return Response<Unit>.Fail(-1);
        }

        Device device = await _notiflowUnitOfWork.DeviceRead.GetCloudMessagePlatformByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (device is null)
        {
            _logger.LogWarning("The customer's device information could not be found.");
            return Response<Unit>.Fail(ResponseCodes.Error.DEVICE_NOT_FOUND);
        }

        var notificationResult = device.CloudMessagePlatform switch
        {
            CloudMessagePlatform.Firesabe => await SendNotifyWithFirebase(request, device.Token, cancellationToken),
            CloudMessagePlatform.Huawei => await SendNotifyWithHuawei(request, device.Token, cancellationToken),
            _ => throw new DeviceException("The cloud messaging platform could not be determined."),
        };

        if (!notificationResult.Succeeded)
        {
            var notificationNotDeliveredEvent = ObjectMapper.Mapper.Map<NotificationNotDeliveredEvent>(request);
            ObjectMapper.Mapper.Map(notificationResult, notificationNotDeliveredEvent);

            await _publishEndpoint.Publish(notificationNotDeliveredEvent, cancellationToken);
            
            _logger.LogWarning("Notification could not be sent to customer with id: {customerId}.", request.CustomerId);

            return Response<Unit>.Fail(ResponseCodes.Error.NOTIFICATION_SENDING_FAILED);
        }

        var notificationDeliveredEvent = ObjectMapper.Mapper.Map<NotificationDeliveredEvent>(request);
        ObjectMapper.Mapper.Map(notificationResult, notificationDeliveredEvent);

        await _publishEndpoint.Publish(notificationDeliveredEvent, cancellationToken);

        _logger.LogInformation("A notification has been sent to the customer with id: {customerId}", request.CustomerId);

        return Response<Unit>.Success(ResponseCodes.Success.NOTIFICATION_SENDING_SUCCESSFUL);
    }

    private async Task<NotificationResult> SendNotifyWithFirebase(SendSingleNotificationCommand request, string deviceToken, CancellationToken cancellationToken)
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

    private async Task<NotificationResult> SendNotifyWithHuawei(SendSingleNotificationCommand request, string deviceToken, CancellationToken cancellationToken)
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