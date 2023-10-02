namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Send;

public sealed class SendTextMessageCommandHandler : IRequestHandler<SendTextMessageCommand, ApiResponse<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ITextMessageService _textMessageService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<SendTextMessageCommandHandler> _logger;

    public SendTextMessageCommandHandler(
        INotiflowUnitOfWork uow,
        ITextMessageService textMessageService,
        IPublishEndpoint publishEndpoint,
        ILogger<SendTextMessageCommandHandler> logger)
    {
        _uow = uow;
        _textMessageService = textMessageService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<ApiResponse<Unit>> Handle(SendTextMessageCommand request, CancellationToken cancellationToken)
    {
        List<string> phoneNumbers = await _uow.CustomerRead.GetPhoneNumbersByIdsAsync(request.CustomerIds, cancellationToken);
        if (phoneNumbers.IsNullOrNotAny())
        {
            _logger.LogWarning("No customers of customer IDs were found. {@customerIds}", request.CustomerIds);
            return ApiResponse<Unit>.Fail(ResponseCodes.Error.CUSTOMERS_PHONE_NUMBERS_NOT_FOUND);
        }

        if (phoneNumbers.Count != request.CustomerIds.Count)
        {
            _logger.LogWarning("The number of customers to send messages to and the number of registered phone numbers do not match.", request.CustomerIds);
            return ApiResponse<Unit>.Fail(ResponseCodes.Error.THE_NUMBER_PHONE_NUMBERS_NOT_EQUAL);
        }

        bool succeeded = await _textMessageService.SendTextMessageAsync(phoneNumbers, request.Message, cancellationToken);
        if (!succeeded)
        {
            await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<TextMessageNotDeliveredEvent>(request), cancellationToken);

            _logger.LogWarning("Sending messages to customers {@CustomerIds} failed.", request.CustomerIds);

            return ApiResponse<Unit>.Fail(ResponseCodes.Error.TEXT_MESSAGE_SENDING_FAILED);
        }

        await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<TextMessageDeliveredEvent>(request), cancellationToken);

        _logger.LogWarning("Message to customers {@CustomerIds} has been sent successfully.", request.CustomerIds);

        return ApiResponse<Unit>.Success(ResponseCodes.Success.TEXT_MESSAGES_SENDING_SUCCESSFUL);
    }
}