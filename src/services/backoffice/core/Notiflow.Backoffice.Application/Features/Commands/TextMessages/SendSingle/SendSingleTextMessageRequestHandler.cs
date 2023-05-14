namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.SendSingle;

public sealed class SendSingleTextMessageRequestHandler : IRequestHandler<SendSingleTextMessageRequest, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ITextMessageService _textMessageService;
    private readonly ILogger<SendSingleTextMessageRequestHandler> _logger;

    public SendSingleTextMessageRequestHandler(
        INotiflowUnitOfWork uow,
        ITextMessageService textMessageService,
        ILogger<SendSingleTextMessageRequestHandler> logger)
    {
        _uow = uow;
        _textMessageService = textMessageService;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(SendSingleTextMessageRequest request, CancellationToken cancellationToken)
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
            _logger.LogWarning("A message could not be sent to the customer with the number {@phoneNumber} and the ID {@customerId}.", phoneNumber, request.CustomerId);
            return Response<Unit>.Fail(-1);
        }

        //Todo: insert text message history

        _logger.LogInformation("A message was sent to customer number {@phoneNumber} and number {@customerId}.", phoneNumber, request.CustomerId);

        return Response<Unit>.Success(-1);
    }
}
