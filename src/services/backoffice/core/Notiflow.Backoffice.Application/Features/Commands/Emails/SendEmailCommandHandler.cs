namespace Notiflow.Backoffice.Application.Features.Commands.Emails;

public sealed record SendEmailCommand(
    string Body,
    string Subject,
    List<int> CustomerIds,
    List<string> CcAddresses,
    List<string> BccAddresses,
    bool IsBodyHtml) : IRequest<Result>;

public sealed class SendEmailCommandHandler(
    INotiflowUnitOfWork uow,
    IEmailService emailService,
    IPublishEndpoint publishEndpoint,
    ILogger<SendEmailCommandHandler> logger) : IRequestHandler<SendEmailCommand, Result>
{
    public async Task<Result> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var emailAddresses = await uow.CustomerRead.GetEmailAddressesByIdsAsync(request.CustomerIds, cancellationToken);
        if (emailAddresses.IsNullOrNotAny())
        {
            return Result.Status404NotFound(ResultCodes.CUSTOMERS_EMAIL_ADDRESSES_NOT_FOUND);
        }

        if (emailAddresses.Count != request.CustomerIds.Count)
        {
            logger.LogWarning("The number of customers to be sent does not match the number of registered mails. Customer IDs: {customerIds}", request.CustomerIds);

            return Result.Status500InternalServerError(ResultCodes.THE_NUMBER_EMAIL_ADDRESSES_NOT_EQUAL);
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
                pipeline.SetAwaitAck(true); // mesajın başarılı bir şekilde işlendiğine dair bir onay (acknowledgement) bekleyeceğinizi belirtir.
                pipeline.Durable = true; // mesajın kalıcı (persistent) olarak yayınlanıp yayınlanmayacağını belirler.
            }, cancellationToken);

            return Result.Status200OK(ResultCodes.EMAIL_SENDING_SUCCESSFUL);
        }

        var emailNotDeliveredEvent = ObjectMapper.Mapper.Map<EmailNotDeliveredEvent>(request);
        emailNotDeliveredEvent.Recipients = emailAddresses;

        await publishEndpoint.Publish(emailNotDeliveredEvent, pipeline =>
        {
            pipeline.SetAwaitAck(true); // mesajın başarılı bir şekilde işlendiğine dair bir onay (acknowledgement) bekleyeceğinizi belirtir.
                                       // Consumer tarafında başarılı işleme durumunda herhangi bir şey yapmanız gerekmez, ancak hata durumunda bir Exception fırlatmanız gerekmektedir.
            pipeline.Durable = true; // mesajın kalıcı (persistent) olarak yayınlanıp yayınlanmayacağını belirler.
        }, cancellationToken);

        return Result.Status500InternalServerError(ResultCodes.EMAIL_SENDING_FAILED);
    }
}

public sealed class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    private const int EMAIL_SUBJECT_MAX_LENGTH = 300;

    public SendEmailCommandValidator()
    {
        RuleForEach(p => p.CustomerIds).Id(FluentVld.Errors.CUSTOMER_ID);
        RuleFor(p => p.Body).Ensure(FluentVld.Errors.EMAIL_BODY);
        RuleFor(p => p.Subject).Ensure(FluentVld.Errors.EMAIL_SUBJECT, EMAIL_SUBJECT_MAX_LENGTH);

        When(p => !p.CcAddresses.IsNullOrNotAny(), () =>
        {
            RuleForEach(p => p.CcAddresses).Email(FluentVld.Errors.EMAIL);
        });

        When(p => !p.BccAddresses.IsNullOrNotAny(), () =>
        {
            RuleForEach(p => p.BccAddresses).Email(FluentVld.Errors.EMAIL);
        });
    }
}