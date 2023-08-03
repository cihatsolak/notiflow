namespace Notiflow.Backoffice.Application.Features.Commands.Emails.Send;

public sealed class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Response<Unit>>
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

    public async Task<Response<Unit>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var emailAddresses = await _uow.CustomerRead.GetEmailAddressesByIdsAsync(request.CustomerIds, cancellationToken);
        if (emailAddresses.IsNullOrNotAny())
        {
            _logger.LogWarning("Customers' e-mail addresses could not be found for sending e-mails. customer ids: {@customerid}", request.CustomerIds);
            
            return Response<Unit>.Fail(-1);
        }

        if (emailAddresses.Count != request.CustomerIds.Count)
        {
            _logger.LogWarning("The number of customers to be sent does not match the number of registered mails.");

            return Response<Unit>.Fail(-1);
        }

        var emailRequest = ObjectMapper.Mapper.Map<EmailRequest>(request);
        emailRequest.ToAddresses = emailAddresses;

        bool succeeded = await _emailService.SendAsync(emailRequest);
        if (!succeeded)
        {
            var emailNotDeliveredEvent = ObjectMapper.Mapper.Map<EmailNotDeliveredEvent>(request);
            ObjectMapper.Mapper.Map(request, emailNotDeliveredEvent);

            await _publishEndpoint.Publish(emailNotDeliveredEvent, cancellationToken);

            _logger.LogWarning("Email sending failed. {@CustomerIds}", request.CustomerIds);

            return Response<Unit>.Fail(1);
            
        }

        var emailDeliveredEvent = ObjectMapper.Mapper.Map<EmailDeliveredEvent>(request);
        ObjectMapper.Mapper.Map(request, emailDeliveredEvent);

        await _publishEndpoint.Publish(emailDeliveredEvent, cancellationToken);

        _logger.LogInformation("Email sending completed successfully. {@CustomerIds}", request.CustomerIds);

        return Response<Unit>.Success(1);
    }
}
