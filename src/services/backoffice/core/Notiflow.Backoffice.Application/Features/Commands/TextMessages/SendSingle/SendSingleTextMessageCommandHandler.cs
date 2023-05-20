namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.SendSingle;

public sealed class SendSingleTextMessageCommandHandler : IRequestHandler<SendSingleTextMessageCommand, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ITextMessageService _textMessageService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<SendSingleTextMessageCommandHandler> _logger;

    public SendSingleTextMessageCommandHandler(
        INotiflowUnitOfWork uow,
        ITextMessageService textMessageService,
        IPublishEndpoint publishEndpoint,
        ILogger<SendSingleTextMessageCommandHandler> logger)
    {
        _uow = uow;
        _textMessageService = textMessageService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(SendSingleTextMessageCommand request, CancellationToken cancellationToken)
    {
        string phoneNumber = await _uow.CustomerRead.GetPhoneNumberByIdAsync(request.CustomerId, cancellationToken);
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            _logger.LogWarning("The phone number of the customer with {@customerId} ID could not be found.", request.CustomerId);
            return Response<Unit>.Fail(-1);
        }

        bool succeeded = await _textMessageService.SendTextMessageAsync(phoneNumber, request.Message);
        if (!succeeded)
        {
            await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<TextMessageNotDeliveredEvent>(request), cancellationToken);
            _logger.LogWarning("A message could not be sent to the customer with the number {@phoneNumber} and the ID {@customerId}.", phoneNumber, request.CustomerId);

            return Response<Unit>.Fail(-1);
        }

        await _publishEndpoint.Publish(ObjectMapper.Mapper.Map<TextMessageDeliveredEvent>(request), cancellationToken);

        _logger.LogInformation("A message was sent to customer number {@phoneNumber} and number {@customerId}.", phoneNumber, request.CustomerId);

        return Response<Unit>.Success(-1);
    }
}
