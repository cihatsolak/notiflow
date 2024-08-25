namespace Notiflow.Backoffice.Application.Features.Commands.Emails;

public sealed record SendEmailCommand(
    string Body,
    string Subject,
    List<int> CustomerIds,
    List<string> CcAddresses,
    List<string> BccAddresses,
    bool IsBodyHtml) : IRequest<Result<Unit>>;

public sealed class SendEmailCommandHandler(
    INotiflowUnitOfWork uow,
    IEmailService emailService,
    IPublishEndpoint publishEndpoint,
    ILogger<SendEmailCommandHandler> logger) : IRequestHandler<SendEmailCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var emailAddresses = await uow.CustomerRead.GetEmailAddressesByIdsAsync(request.CustomerIds, cancellationToken);
        if (emailAddresses.IsNullOrNotAny())
        {
            return Result<Unit>.Status404NotFound(ResultCodes.CUSTOMERS_EMAIL_ADDRESSES_NOT_FOUND);
        }

        if (emailAddresses.Count != request.CustomerIds.Count)
        {
            logger.LogWarning("The number of customers to be sent does not match the number of registered mails. Customer IDs: {customerIds}", request.CustomerIds);

            return Result<Unit>.Status500InternalServerError(ResultCodes.THE_NUMBER_EMAIL_ADDRESSES_NOT_EQUAL);
        }

        var emailRequest = ObjectMapper.Mapper.Map<EmailRequest>(request);
        emailRequest.Recipients = emailAddresses;

        bool succeeded = await emailService.SendAsync(emailRequest, cancellationToken);
        if (succeeded)
        {
            var emailDeliveredEvent = ObjectMapper.Mapper.Map<EmailDeliveredEvent>(request);
            emailDeliveredEvent.Recipients = emailAddresses;

            await publishEndpoint.Publish(emailDeliveredEvent, pipeline =>
            {
                pipeline.SetAwaitAck(false);
                pipeline.Durable = true;
            }, cancellationToken);

            return Result<Unit>.Status200OK(ResultCodes.EMAIL_SENDING_SUCCESSFUL);
        }

        var emailNotDeliveredEvent = ObjectMapper.Mapper.Map<EmailNotDeliveredEvent>(request);
        emailNotDeliveredEvent.Recipients = emailAddresses;

        await publishEndpoint.Publish(emailNotDeliveredEvent, pipeline =>
        {
            pipeline.SetAwaitAck(false);
            pipeline.Durable = true;
        }, cancellationToken);

        return Result<Unit>.Status500InternalServerError(ResultCodes.EMAIL_SENDING_FAILED);
    }
}

public sealed class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    private const int EMAIL_SUBJECT_MAX_LENGTH = 300;

    public SendEmailCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);
        RuleFor(p => p.Body).Ensure(localizer[ValidationErrorMessage.EMAIL_BODY]);
        RuleFor(p => p.Subject).Ensure(localizer[ValidationErrorMessage.EMAIL_SUBJECT], EMAIL_SUBJECT_MAX_LENGTH);

        When(p => !p.CcAddresses.IsNullOrNotAny(), () =>
        {
            RuleForEach(p => p.CcAddresses).Email(localizer[ValidationErrorMessage.EMAIL]);
        });

        When(p => !p.BccAddresses.IsNullOrNotAny(), () =>
        {
            RuleForEach(p => p.BccAddresses).Email(localizer[ValidationErrorMessage.EMAIL]);
        });
    }
}