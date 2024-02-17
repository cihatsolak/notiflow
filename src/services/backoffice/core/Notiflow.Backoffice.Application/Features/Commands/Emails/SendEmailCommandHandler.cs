namespace Notiflow.Backoffice.Application.Features.Commands.Emails;

public sealed record SendEmailCommand(
    string Body,
    string Subject,
    List<int> CustomerIds,
    List<string> CcAddresses,
    List<string> BccAddresses,
    bool IsBodyHtml) : IRequest<Result<Unit>>;

public sealed class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly IEmailService _emailService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<SendEmailCommandHandler> _logger;

    public SendEmailCommandHandler(
        INotiflowUnitOfWork uow,
        IEmailService emailService,
        IPublishEndpoint publishEndpoint,
        ILogger<SendEmailCommandHandler> logger)
    {
        _uow = uow;
        _emailService = emailService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var emailAddresses = await _uow.CustomerRead.GetEmailAddressesByIdsAsync(request.CustomerIds, cancellationToken);
        if (emailAddresses.IsNullOrNotAny())
        {
            return Result<Unit>.Status404NotFound(ResultCodes.CUSTOMERS_EMAIL_ADDRESSES_NOT_FOUND);
        }

        if (emailAddresses.Count != request.CustomerIds.Count)
        {
            _logger.LogWarning("The number of customers to be sent does not match the number of registered mails. Customer IDs: {customerIds}", request.CustomerIds);

            return Result<Unit>.Status500InternalServerError(ResultCodes.THE_NUMBER_EMAIL_ADDRESSES_NOT_EQUAL);
        }

        var emailRequest = ObjectMapper.Mapper.Map<EmailRequest>(request);
        emailRequest.Recipients = emailAddresses;

        bool succeeded = await _emailService.SendAsync(emailRequest, cancellationToken);
        if (!succeeded)
        {
            return await ReportFailedStatusAsync(request, emailAddresses, cancellationToken);
        }

        return await ReportSuccessfulStatusAsync(request, emailAddresses, cancellationToken);
    }

    private async Task<Result<Unit>> ReportFailedStatusAsync(SendEmailCommand request, List<string> emailAddresses, CancellationToken cancellationToken)
    {
        var emailNotDeliveredEvent = ObjectMapper.Mapper.Map<EmailNotDeliveredEvent>(request);
        emailNotDeliveredEvent.Recipients = emailAddresses;

        await _publishEndpoint.Publish(emailNotDeliveredEvent, cancellationToken);

        return Result<Unit>.Status500InternalServerError(ResultCodes.EMAIL_SENDING_FAILED);
    }

    private async Task<Result<Unit>> ReportSuccessfulStatusAsync(SendEmailCommand request, List<string> emailAddresses, CancellationToken cancellationToken)
    {
        var emailDeliveredEvent = ObjectMapper.Mapper.Map<EmailDeliveredEvent>(request);
        emailDeliveredEvent.Recipients = emailAddresses;

        await _publishEndpoint.Publish(emailDeliveredEvent, cancellationToken);

        return Result<Unit>.Status200OK(ResultCodes.EMAIL_SENDING_SUCCESSFUL);
    }
}
