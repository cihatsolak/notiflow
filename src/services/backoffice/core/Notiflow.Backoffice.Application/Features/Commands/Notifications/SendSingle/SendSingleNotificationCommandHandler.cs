using Notiflow.Backoffice.Application.Models.Notifications;

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
        _huaweiService = huaweiService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(SendSingleNotificationCommand request, CancellationToken cancellationToken)
    {
        Device device = await _notiflowUnitOfWork.DeviceRead.GetCloudMessagePlatformByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (device is null)
        {
            _logger.LogWarning("The customer's device information could not be found.");
            return Response<Unit>.Fail(1);
        }

        var notificationResult = device.CloudMessagePlatform switch
        {
            CloudMessagePlatform.Firesabe => await SendNotifyWithFirebase(request, device.Token, cancellationToken),
            CloudMessagePlatform.Huawei => await SendNotifyWithHuawei(request, device.Token, cancellationToken),
            _ => throw new Exception(""),
        };

        if (!notificationResult.Succeeded)
        {
            var notificationNotDeliveredEvent = ObjectMapper.Mapper.Map<NotificationNotDeliveredEvent>(request);
            ObjectMapper.Mapper.Map(notificationResult, notificationNotDeliveredEvent);

            await _publishEndpoint.Publish(notificationNotDeliveredEvent, cancellationToken);
            
            _logger.LogWarning("Notification could not be sent to customer with id: {customerId}.", request.CustomerId);

            return Response<Unit>.Fail(-1);
        }

        var notificationDeliveredEvent = ObjectMapper.Mapper.Map<NotificationDeliveredEvent>(request);
        ObjectMapper.Mapper.Map(notificationResult, notificationDeliveredEvent);

        await _publishEndpoint.Publish(notificationDeliveredEvent, cancellationToken);

        _logger.LogInformation("A notification has been sent to the customer with id: {customerId}", request.CustomerId);

        return Response<Unit>.Success(1);
    }

    private async Task<NotificationResult> SendNotifyWithFirebase(SendSingleNotificationCommand request, string deviceToken, CancellationToken cancellationToken)
    {
        Guid secretIdentity = Guid.NewGuid();

        FirebaseSingleNotificationRequest firebaseSingleNotificationRequest = new()
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
        Guid secretIdentity = Guid.NewGuid();

        HuaweiNotificationRequest huaweiNotificationRequest = new()
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

        var huaweiResponse = await _huaweiService.SendNotificationAsync(huaweiNotificationRequest, cancellationToken);
        if (huaweiResponse is null)
        {
            _logger.LogInformation("");
            return new NotificationResult();
        }

        return new NotificationResult
        {
            Succeeded = huaweiResponse.Succeeded,
            ErrorMessage = huaweiResponse.ErrorMessage,
            SecretIdentity = secretIdentity
        };
    }
}