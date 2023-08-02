namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.SendSingle;

public sealed class SendSingleNotificationCommandHandler : IRequestHandler<SendSingleNotificationCommand, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _notiflowUnitOfWork;
    private readonly IFirebaseService _firebaseService;
    private readonly IHuaweiService _huaweiService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<SendSingleNotificationCommandHandler> _logger;

    public SendSingleNotificationCommandHandler(
        INotiflowUnitOfWork notiflowUnitOfWork,
        IFirebaseService firebaseService,
        IHuaweiService huaweiService,
        IPublishEndpoint publishEndpoint,
        ILogger<SendSingleNotificationCommandHandler> logger)
    {
        _notiflowUnitOfWork = notiflowUnitOfWork;
        _firebaseService = firebaseService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(SendSingleNotificationCommand request, CancellationToken cancellationToken)
    {
        Device device = await _notiflowUnitOfWork.DeviceRead.GetCloudMessagePlatformByCustomerIdAsync(request.CustomerId, cancellationToken);

        var result = device.CloudMessagePlatform switch
        {
            CloudMessagePlatform.Firesabe => await SendNotifyWithFirebase(request, device.Token, cancellationToken),
            CloudMessagePlatform.Huawei => await SendNotifyWithHuawei(request, device.Token, cancellationToken),
            _ => throw new Exception(""),
        };

        if (!result.Succeeded)
        {
            var notificationNotDeliveredEvent = ObjectMapper.Mapper.Map<NotificationNotDeliveredEvent>(request);
            notificationNotDeliveredEvent.ErrorMessage = result.ErrorMessage;

            await _publishEndpoint.Publish(notificationNotDeliveredEvent, cancellationToken);
            
            _logger.LogWarning("Notification could not be sent to customer with id: {customerId}.", request.CustomerId);

            return Response<Unit>.Fail(-1);
        }

        await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<NotificationDeliveredEvent>(request), cancellationToken);

        _logger.LogInformation("A notification has been sent to the customer with id: {customerId}", request.CustomerId);

        return Response<Unit>.Success(1);
    }

    private async Task<NotificationResult> SendNotifyWithFirebase(SendSingleNotificationCommand request, string deviceToken, CancellationToken cancellationToken)
    {
        Guid secretIdentity = Guid.NewGuid();

        FirebaseSingleNotificationRequest firebaseSingleNotificationRequest = new FirebaseSingleNotificationRequest()
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

        var firebaseResponse = await _firebaseService.SendNotificationAsync(firebaseSingleNotificationRequest, cancellationToken);
        if (firebaseResponse is null)
        {
            _logger.LogInformation("");
            return new NotificationResult();
        }

        return new NotificationResult
        {
            Succeeded = firebaseResponse.Succeeded,
            ErrorMessage = firebaseResponse.Succeeded ? null : firebaseResponse.Results[0].ErrorMessage,
            SecretIdentity = secretIdentity
        };
    }

    private async Task<NotificationResult> SendNotifyWithHuawei(SendSingleNotificationCommand request, string deviceToken, CancellationToken cancellationToken)
    {
        HuaweiNotificationRequest huaweiNotificationRequest = new()
        {
            Message = new HuaweiMessage
            {
                DeviceTokens = new List<string>
                {
                    deviceToken
                }
            }
        };

        return Response<Unit>.Success(1);
    }
}