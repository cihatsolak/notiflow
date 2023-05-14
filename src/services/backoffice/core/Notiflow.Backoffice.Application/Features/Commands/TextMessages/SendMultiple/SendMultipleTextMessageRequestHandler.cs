namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.SendMultiple;

public sealed class SendMultipleTextMessageRequestHandler : IRequestHandler<SendMultipleTextMessageRequest, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ITextMessageService _textMessageService;
    private readonly ILogger<SendMultipleTextMessageRequestHandler> _logger;

    public SendMultipleTextMessageRequestHandler(
        INotiflowUnitOfWork uow,
        ITextMessageService textMessageService,
        ILogger<SendMultipleTextMessageRequestHandler> logger)
    {
        _uow = uow;
        _textMessageService = textMessageService;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(SendMultipleTextMessageRequest request, CancellationToken cancellationToken)
    {
        var customers = await _uow.CustomerRead.GetPhoneNumbersByIdsAsync(request.CustomerIds, cancellationToken);
        if (customers is null) //Todo: IsNullOrNotAny
        {
            _logger.LogWarning("The phone numbers of the customer IDs could not be found. customer IDs: {@customerIds}", string.Join(',', request.CustomerIds));
            return Response<Unit>.Fail(-1);
        }

        var phoneNumbers = customers.Select(customer => customer.PhoneNumber);
        if (phoneNumbers is null) //Todo: IsNullOrNotAny
        {
            _logger.LogWarning("Customer phone numbers could not be selected. customer IDs: {@customerIds}", string.Join(',', request.CustomerIds));
            return Response<Unit>.Fail(-1);
        }

        bool succeeded = await _textMessageService.SendTextMessageAsync(phoneNumbers, request.Message);
        if (!succeeded)
        {
            _logger.LogWarning("Failed to send messages to {@phoneNumbers}.", phoneNumbers);
            return Response<Unit>.Fail(-1);
        }

        //Todo: insert text message history

        _logger.LogInformation("A message was sent to customer number {@phoneNumber} and number {@customerId}.", phoneNumbers, request.CustomerIds); //Todo: edit log

        return Response<Unit>.Success(-1);
    }
}
