namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Send;

public sealed class SendTextMessageCommandHandler : IRequestHandler<SendTextMessageCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ITextMessageService _textMessageService;
    private readonly ILocalizerService<ValidationErrorCodes> _localizer;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<SendTextMessageCommandHandler> _logger;

    public SendTextMessageCommandHandler(
        INotiflowUnitOfWork uow,
        ITextMessageService textMessageService,
        ILocalizerService<ValidationErrorCodes> localizer,
        IPublishEndpoint publishEndpoint,
        ILogger<SendTextMessageCommandHandler> logger)
    {
        _uow = uow;
        _textMessageService = textMessageService;
        _localizer = localizer;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(SendTextMessageCommand request, CancellationToken cancellationToken)
    {
        List<string> phoneNumbers = await _uow.CustomerRead.GetPhoneNumbersByIdsAsync(request.CustomerIds, cancellationToken);
        if (phoneNumbers.IsNullOrNotAny())
        {
            return Result<Unit>.Failure(StatusCodes.Status404NotFound, _localizer[ValidationErrorCodes.CUSTOMERS_PHONE_NUMBERS_NOT_FOUND]);
        }

        if (phoneNumbers.Count != request.CustomerIds.Count)
        {
            _logger.LogWarning("The number of customers to send messages to and the number of registered phone numbers do not match. Customer IDs: {CustomerIds}.", request.CustomerIds);
            return Result<Unit>.Failure(StatusCodes.Status500InternalServerError, _localizer[ValidationErrorCodes.THE_NUMBER_PHONE_NUMBERS_NOT_EQUAL]);
        }

        bool succeeded = await _textMessageService.SendTextMessageAsync(phoneNumbers, request.Message, cancellationToken);
        if (!succeeded)
        {
            await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<TextMessageNotDeliveredEvent>(request), cancellationToken);
            
            return Result<Unit>.Failure(StatusCodes.Status500InternalServerError, _localizer[ValidationErrorCodes.TEXT_MESSAGE_SENDING_FAILED]);
        }

        await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<TextMessageDeliveredEvent>(request), cancellationToken);
        
        return Result<Unit>.Success(StatusCodes.Status200OK, _localizer[ValidationErrorCodes.TEXT_MESSAGES_SENDING_SUCCESSFUL], Unit.Value);
    }
}