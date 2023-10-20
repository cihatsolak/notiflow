namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdatePhoneNumber;

public sealed class UpdateCustomerPhoneNumberCommandHandler : IRequestHandler<UpdateCustomerPhoneNumberCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultState> _localizer;
    private readonly ILogger<UpdateCustomerPhoneNumberCommandHandler> _logger;

    public UpdateCustomerPhoneNumberCommandHandler(
        INotiflowUnitOfWork uow, 
        ILocalizerService<ResultState> localizer, 
        ILogger<UpdateCustomerPhoneNumberCommandHandler> logger)
    {
        _uow = uow;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateCustomerPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<Unit>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.CUSTOMER_NOT_FOUND]);
        }

        if (string.Equals(customer.PhoneNumber, request.PhoneNumber))
        {
            _logger.LogWarning("The phone number to be changed is the same as in the current one. Customer ID: {id}", request.Id);
            return Result<Unit>.Failure(StatusCodes.Status400BadRequest, _localizer[ResultState.CUSTOMER_PHONE_NUMBER_SAME]);
        }

        customer.PhoneNumber = request.PhoneNumber;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The customer's phone number has been updated. ID: {id}", request.Id);

        return Result<Unit>.Success(StatusCodes.Status204NoContent, _localizer[ResultState.CUSTOMER_PHONE_NUMBER_UPDATED], Unit.Value);
    }
}
