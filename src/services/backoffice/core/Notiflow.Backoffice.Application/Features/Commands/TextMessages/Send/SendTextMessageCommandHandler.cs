namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Send;

public sealed class SendTextMessageCommandHandler : IRequestHandler<SendTextMessageCommand, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ITextMessageService _textMessageService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IRedisService _redisService;
    private readonly ILogger<SendTextMessageCommandHandler> _logger;

    public SendTextMessageCommandHandler(
        INotiflowUnitOfWork uow,
        ITextMessageService textMessageService,
        IPublishEndpoint publishEndpoint,
        IRedisService redisService,
        ILogger<SendTextMessageCommandHandler> logger)
    {
        _uow = uow;
        _textMessageService = textMessageService;
        _publishEndpoint = publishEndpoint;
        _redisService = redisService;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(SendTextMessageCommand request, CancellationToken cancellationToken)
    {
        bool isSentMessageAllowed = await _redisService.HashGetAsync<bool>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_MESSAGE_PERMISSION);
        if (!isSentMessageAllowed)
        {
            _logger.LogWarning("The tenant is not authorized to send messages.");
            return Response<Unit>.Fail(-1);
        }

        List<string> phoneNumbers = await _uow.CustomerRead.GetPhoneNumbersByIdsAsync(request.CustomerIds, cancellationToken);
        if (phoneNumbers.IsNullOrNotAny())
        {
            _logger.LogWarning("No customers of customer IDs were found. {@customerIds}", request.CustomerIds);
            return Response<Unit>.Fail(ResponseCodes.Error.CUSTOMERS_PHONE_NUMBERS_NOT_FOUND);
        }

        if (phoneNumbers.Count != request.CustomerIds.Count)
        {
            _logger.LogWarning("The number of customers to send messages to and the number of registered phone numbers do not match.", request.CustomerIds);
            return Response<Unit>.Fail(ResponseCodes.Error.THE_NUMBER_PHONE_NUMBERS_NOT_EQUAL);
        }

        bool succeeded = await _textMessageService.SendTextMessageAsync(phoneNumbers, request.Message, cancellationToken);
        if (!succeeded)
        {
            await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<TextMessageNotDeliveredEvent>(request), cancellationToken);

            _logger.LogWarning("Sending messages to customers {@CustomerIds} failed.", request.CustomerIds);

            return Response<Unit>.Fail(ResponseCodes.Error.TEXT_MESSAGE_SENDING_FAILED);
        }

        await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<TextMessageDeliveredEvent>(request), cancellationToken);

        _logger.LogWarning("Message to customers {@CustomerIds} has been sent successfully.", request.CustomerIds);

        return Response<Unit>.Success(ResponseCodes.Success.TEXT_MESSAGES_SENDING_SUCCESSFUL);
    }
}