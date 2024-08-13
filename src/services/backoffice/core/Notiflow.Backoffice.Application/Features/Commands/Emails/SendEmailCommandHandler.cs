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
        if (!succeeded)
        {
            return await ReportFailedAsync(request, emailAddresses, cancellationToken);
        }

        return await ReportStatusAsync(request, emailAddresses, cancellationToken);
    }

    private async Task<Result<Unit>> ReportFailedAsync(SendEmailCommand request, List<string> emailAddresses, CancellationToken cancellationToken)
    {
        var emailNotDeliveredEvent = ObjectMapper.Mapper.Map<EmailNotDeliveredEvent>(request);
        emailNotDeliveredEvent.Recipients = emailAddresses;

        await publishEndpoint.Publish(emailNotDeliveredEvent, cancellationToken);

        return Result<Unit>.Status500InternalServerError(ResultCodes.EMAIL_SENDING_FAILED);
    }

    private async Task<Result<Unit>> ReportStatusAsync(SendEmailCommand request, List<string> emailAddresses, CancellationToken cancellationToken)
    {
        var emailDeliveredEvent = ObjectMapper.Mapper.Map<EmailDeliveredEvent>(request);
        emailDeliveredEvent.Recipients = emailAddresses;

        await publishEndpoint.Publish(emailDeliveredEvent, cancellationToken);

        return Result<Unit>.Status200OK(ResultCodes.EMAIL_SENDING_SUCCESSFUL);
    }
}
