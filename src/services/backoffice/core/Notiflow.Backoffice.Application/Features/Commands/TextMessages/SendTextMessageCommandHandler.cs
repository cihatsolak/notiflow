namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages;

public sealed record SendTextMessageCommand(List<int> CustomerIds, string Message) : IRequest<Result<Unit>>;

public sealed class SendTextMessageCommandHandler : IRequestHandler<SendTextMessageCommand, Result<Unit>>
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

    public async Task<Result<Unit>> Handle(SendTextMessageCommand request, CancellationToken cancellationToken)
    {
        List<string> phoneNumbers = await _uow.CustomerRead.GetPhoneNumbersByIdsAsync(request.CustomerIds, cancellationToken);
        if (phoneNumbers.IsNullOrNotAny())
        {
            return Result<Unit>.Status404NotFound(ResultCodes.CUSTOMERS_PHONE_NUMBERS_NOT_FOUND);
        }

        if (phoneNumbers.Count != request.CustomerIds.Count)
        {
            _logger.LogWarning("The number of customers to send messages to and the number of registered phone numbers do not match. Customer IDs: {CustomerIds}.", request.CustomerIds);
            return Result<Unit>.Status500InternalServerError(ResultCodes.THE_NUMBER_PHONE_NUMBERS_NOT_EQUAL);
        }

        bool succeeded = await _textMessageService.SendTextMessageAsync(phoneNumbers, request.Message, cancellationToken);
        if (!succeeded)
        {
            await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<TextMessageNotDeliveredEvent>(request), cancellationToken);

            return Result<Unit>.Status500InternalServerError(ResultCodes.TEXT_MESSAGE_SENDING_FAILED);
        }

        await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<TextMessageDeliveredEvent>(request), cancellationToken);

        return Result<Unit>.Status200OK(ResultCodes.TEXT_MESSAGES_SENDING_SUCCESSFUL);
    }
}

public sealed class SendTextMessageCommandValidator : AbstractValidator<SendTextMessageCommand>
{
    public SendTextMessageCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);

        RuleFor(p => p.Message)
           .NotNullAndNotEmpty(localizer[ValidationErrorMessage.TEXT_MESSAGE])
           .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.TEXT_MESSAGE]);
    }
}