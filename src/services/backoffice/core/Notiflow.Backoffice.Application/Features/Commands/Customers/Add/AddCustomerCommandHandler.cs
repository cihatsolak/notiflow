namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Add;

public sealed class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Result<int>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ValidationErrorCodes> _localizer;
    private readonly ILogger<AddCustomerCommandHandler> _logger;

    public AddCustomerCommandHandler(
        INotiflowUnitOfWork uow,
        ILocalizerService<ValidationErrorCodes> localizer,
        ILogger<AddCustomerCommandHandler> logger)
    {
        _uow = uow;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isExists = await _uow.CustomerRead.IsExistsByPhoneNumberOrEmailAsync(request.PhoneNumber, request.Email, cancellationToken);
        if (isExists)
        {
            return Result<int>.Failure(StatusCodes.Status400BadRequest, _localizer[ValidationErrorCodes.CUSTOMER_EXISTS]);
        }

        var customer = ObjectMapper.Mapper.Map<Customer>(request);

        await _uow.CustomerWrite.InsertAsync(customer, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("A new customer with {customerId} id has been registered.", customer.Id);

        return Result<int>.Success(StatusCodes.Status201Created, _localizer[ValidationErrorCodes.CUSTOMER_ADDED], customer.Id);
    }
}