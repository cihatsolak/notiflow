namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Send;

public sealed class SendTextMessageCommandHandler : IRequestHandler<SendTextMessageCommand, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ITextMessageService _textMessageService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IRedisService _redisService;
    private readonly ILogger<SendTextMessageCommandHandler> _logger;

    public SendTextMessageCommandHandler(
        INotiflowUnitOfWork uow,
        ITextMessageService textMessageService,
        IPublishEndpoint publishEndpoint,
        IRedisService redisService,

        ILogger<SendTextMessageCommandHandler> logger)
    {
        _uow = uow;
        _textMessageService = textMessageService;
        _publishEndpoint = publishEndpoint;
        _redisService = redisService;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(SendTextMessageCommand request, CancellationToken cancellationToken)
    {
        bool isSentMessageAllowed = await _redisService.HashGetAsync<bool>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_PERMISSION), CacheKeys.MESSAGE_PERMISSION);
        if (!isSentMessageAllowed)
        {
            _logger.LogWarning("The tenant is not authorized to send messages.");
            return Response<Unit>.Fail(-1);
        }

        List<Customer> customers = await _uow.CustomerRead.GetPhoneNumbersByIdsAsync(request.CustomerIds, cancellationToken);
        if (customers.IsNullOrNotAny())
        {
            _logger.LogWarning("No customers of customer IDs were found. {@customerIds}", request.CustomerIds);
            return Response<Unit>.Fail(-1);
        }

        if (customers.Count != request.CustomerIds.Count)
        {
            _logger.LogWarning("The number of customers to send messages to and the number of registered phone numbers do not match.", request.CustomerIds);
            return Response<Unit>.Fail(-1);
        }

        TextMessageRequest textMessageRequest = new()
        {
            Message = request.Message,
            PhoneNumbers = customers.Select(p => p.PhoneNumber)
        };

        List<TextMessageResult> textMessageResults = await _textMessageService.SendTextMessageAsync(textMessageRequest, cancellationToken);

        await ReportFailedStatusAsync(textMessageResults, customers, request, cancellationToken);

        await ReportSuccessfulStatusAsync(textMessageResults, customers, request, cancellationToken);

        return Response<Unit>.Success(-1);
    }

    private async Task ReportFailedStatusAsync(IEnumerable<TextMessageResult> textMessageResults, List<Customer> customers, SendTextMessageCommand request, CancellationToken cancellationToken)
    {
        var failedTextMessageResults = textMessageResults.Where(result => !result.IsSent);
        if (!failedTextMessageResults.Any())
            return;

        var textMessageNotDeliveredEvents = failedTextMessageResults.Select(failedResult => new TextMessageNotDeliveredEvent
        {
            CustomerId = customers.Single(customer => customer.PhoneNumber == failedResult.PhoneNumber).Id,
            Message = request.Message
        });

        await _publishEndpoint.Publish(textMessageNotDeliveredEvents, cancellationToken);

        _logger.LogWarning("Sending messages to users {@CustomerIds} failed.", request.CustomerIds);
    }

    private async Task ReportSuccessfulStatusAsync(IEnumerable<TextMessageResult> textMessageResults, List<Customer> customers, SendTextMessageCommand request, CancellationToken cancellationToken)
    {
        var successfulTextMessageResults = textMessageResults.Where(result => result.IsSent);
        if (!successfulTextMessageResults.Any())
            return;

        var textMessageDeliveredEvents = successfulTextMessageResults.Select(successfulResult => new TextMessageDeliveredEvent
        {
            CustomerId = customers.Single(customer => customer.PhoneNumber == successfulResult.PhoneNumber).Id,
            Message = request.Message
        });

        await _publishEndpoint.Publish(textMessageDeliveredEvents, cancellationToken);

        _logger.LogWarning("Message to customers {@CustomerIds} has been sent successfully.", request.CustomerIds);
    }
}
