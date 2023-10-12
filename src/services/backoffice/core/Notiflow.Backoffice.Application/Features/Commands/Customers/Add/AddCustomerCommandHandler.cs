namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Add;

public sealed class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, ApiResponse<int>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<AddCustomerCommandHandler> _logger;

    public AddCustomerCommandHandler(
        INotiflowUnitOfWork uow, 
        ILogger<AddCustomerCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<ApiResponse<int>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isExists = await _uow.CustomerRead.IsExistsByPhoneNumberOrEmailAsync(request.PhoneNumber, request.Email, cancellationToken);
        if (isExists)
        {
            return ApiResponse<int>.Fail(ResponseCodes.Error.CUSTOMER_EXISTS);
        }

        var customer = ObjectMapper.Mapper.Map<Customer>(request);

        await _uow.CustomerWrite.InsertAsync(customer, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("A new customer with {customerId} id has been registered.", customer.Id);

        return ApiResponse<int>.Success(ResponseCodes.Success.CUSTOMER_ADDED, customer.Id);
    }
}