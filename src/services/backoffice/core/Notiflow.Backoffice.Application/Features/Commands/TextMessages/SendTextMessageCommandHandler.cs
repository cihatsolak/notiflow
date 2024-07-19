namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages;

public sealed record SendTextMessageCommand(List<int> CustomerIds, string Message) : IRequest<Result<Unit>>;

public sealed class SendTextMessageCommandHandler(
    INotiflowUnitOfWork uow,
    ITextMessageService textMessageService,
    IPublishEndpoint publishEndpoint,
    ILogger<SendTextMessageCommandHandler> logger) : IRequestHandler<SendTextMessageCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(SendTextMessageCommand request, CancellationToken cancellationToken)
    {
        List<string> phoneNumbers = await uow.CustomerRead.GetPhoneNumbersByIdsAsync(request.CustomerIds, cancellationToken);
        if (phoneNumbers.IsNullOrNotAny())
        {
            return Result<Unit>.Status404NotFound(ResultCodes.CUSTOMERS_PHONE_NUMBERS_NOT_FOUND);
        }

        if (phoneNumbers.Count != request.CustomerIds.Count)
        {
            logger.LogWarning("The number of customers to send messages to and the number of registered phone numbers do not match. Customer IDs: {CustomerIds}.", request.CustomerIds);
            return Result<Unit>.Status500InternalServerError(ResultCodes.THE_NUMBER_PHONE_NUMBERS_NOT_EQUAL);
        }

        bool succeeded = await textMessageService.SendTextMessageAsync(phoneNumbers, request.Message, cancellationToken);
        if (!succeeded)
        {
            await publishEndpoint.Publish(ObjectMapper.Mapper.Map<TextMessageNotDeliveredEvent>(request), cancellationToken);

            return Result<Unit>.Status500InternalServerError(ResultCodes.TEXT_MESSAGE_SENDING_FAILED);
        }

        await publishEndpoint.Publish(ObjectMapper.Mapper.Map<TextMessageDeliveredEvent>(request), cancellationToken);

        return Result<Unit>.Status200OK(ResultCodes.TEXT_MESSAGES_SENDING_SUCCESSFUL);
    }
}

public sealed class SendTextMessageCommandValidator : AbstractValidator<SendTextMessageCommand>
{
    private const int TEXT_MESSAGE_MAX_LENGTH = 300;

    public SendTextMessageCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);
        RuleFor(p => p.Message).Ensure(localizer[ValidationErrorMessage.TEXT_MESSAGE], TEXT_MESSAGE_MAX_LENGTH);
    }
}