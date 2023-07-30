namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.SendMultiple;

public sealed class SendMultipleTextMessageCommandHandler : IRequestHandler<SendMultipleTextMessageCommand, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ITextMessageService _textMessageService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IRedisService _redisService;
    private readonly ILogger<SendMultipleTextMessageCommandHandler> _logger;

    public SendMultipleTextMessageCommandHandler(
        INotiflowUnitOfWork uow,
        ITextMessageService textMessageService,
        IPublishEndpoint publishEndpoint,
        IRedisService redisService,
        ILogger<SendMultipleTextMessageCommandHandler> logger)
    {
        _uow = uow;
        _textMessageService = textMessageService;
        _publishEndpoint = publishEndpoint;
        _redisService = redisService;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(SendMultipleTextMessageCommand request, CancellationToken cancellationToken)
    {
        bool isSentMessageAllowed = await _redisService.HashGetAsync<bool>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_PERMISSION), CacheKeys.MESSAGE_PERMISSION);
        if (!isSentMessageAllowed)
        {
            _logger.LogWarning("The tenant is not authorized to send messages.");
            return Response<Unit>.Fail(-1);
        }

        var customers = await _uow.CustomerRead.GetPhoneNumbersByIdsAsync(request.CustomerIds, cancellationToken);
        if (customers.IsNullOrNotAny())
        {
            _logger.LogWarning("The phone numbers of the customer IDs could not be found. customer IDs: {@customerIds}", request.CustomerIds);
            return Response<Unit>.Fail(-1);
        }

        List<Task> messageSendingResultTasks = new();

        foreach (var customer in customers)
        {
            TextMessageNotDeliveredEvent textMessageNotDeliveredEvent = new()
            {
                CustomerId = customer.Id,
                Message = request.Message
            };

            bool succeeded = await _textMessageService.SendTextMessageAsync(customer.PhoneNumber, request.Message, cancellationToken);
            if (!succeeded)
            {
                messageSendingResultTasks.Add(
                    _publishEndpoint.Publish(textMessageNotDeliveredEvent, cancellationToken)
                    );

                _logger.LogWarning("Failed to send messages to {@phoneNumber}.", customer.PhoneNumber);
            }
            else
            {
                messageSendingResultTasks.Add(
                   _publishEndpoint.Publish(textMessageNotDeliveredEvent, cancellationToken)
                   );

                _logger.LogInformation("A message was sent to phone number {@phoneNumber}.", customer.PhoneNumber);
            }
        }

        await Task.WhenAll(messageSendingResultTasks);

        _logger.LogInformation("multi-message sending completed.");

        return Response<Unit>.Success(-1);
    }
}
