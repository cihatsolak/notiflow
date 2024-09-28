namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages;

public sealed record SendTextMessageCommand(List<int> CustomerIds, string Message) : IRequest<Result>;

public sealed class SendTextMessageCommandHandler(
    INotiflowUnitOfWork uow,
    ITextMessageService textMessageService,
    IPublishEndpoint publishEndpoint,
    ILogger<SendTextMessageCommandHandler> logger) : IRequestHandler<SendTextMessageCommand, Result>
{
    public async Task<Result> Handle(SendTextMessageCommand request, CancellationToken cancellationToken)
    {
        List<string> phoneNumbers = await uow.CustomerRead.GetPhoneNumbersByIdsAsync(request.CustomerIds, cancellationToken);
        if (phoneNumbers.IsNullOrNotAny())
        {
            return Result.Status404NotFound(ResultCodes.CUSTOMERS_PHONE_NUMBERS_NOT_FOUND);
        }

        if (phoneNumbers.Count != request.CustomerIds.Count)
        {
            logger.LogWarning("The number of customers to send messages to and the number of registered phone numbers do not match. Customer IDs: {CustomerIds}.", request.CustomerIds);
            return Result.Status500InternalServerError(ResultCodes.THE_NUMBER_PHONE_NUMBERS_NOT_EQUAL);
        }

        bool succeeded = await textMessageService.SendTextMessageAsync(phoneNumbers, request.Message, cancellationToken);
        if (succeeded)
        {
            var textMessageDeliveredEvent = ObjectMapper.Mapper.Map<TextMessageDeliveredEvent>(request);

            await publishEndpoint.Publish(textMessageDeliveredEvent, pipeline =>
            {
                pipeline.SetAwaitAck(true);
                pipeline.Durable = true;
            }, cancellationToken);

            return Result.Status200OK(ResultCodes.TEXT_MESSAGES_SENDING_SUCCESSFUL);
        }

        var textMessageNotDeliveredEvent = ObjectMapper.Mapper.Map<TextMessageNotDeliveredEvent>(request);
        await publishEndpoint.Publish(textMessageNotDeliveredEvent, pipeline =>
        {
            pipeline.SetAwaitAck(true);
            pipeline.Durable = true;
        }, cancellationToken);

        return Result.Status500InternalServerError(ResultCodes.TEXT_MESSAGE_SENDING_FAILED);
    }
}

public sealed class SendTextMessageCommandValidator : AbstractValidator<SendTextMessageCommand>
{
    public SendTextMessageCommandValidator()
    {
        RuleForEach(p => p.CustomerIds).Id(FluentVld.Errors.CUSTOMER_ID);
        RuleFor(p => p.Message).Ensure(FluentVld.Errors.TEXT_MESSAGE, FluentVld.Rules.TEXT_MESSAGE_MAX_300_LENGTH);
    }
}