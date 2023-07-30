namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Add;

public sealed class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Response<int>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<AddCustomerCommandHandler> _logger;

    public AddCustomerCommandHandler(INotiflowUnitOfWork uow, ILogger<AddCustomerCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<int>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isExists = await _uow.CustomerRead.IsExistsByPhoneNumberOrEmailAsync(request.PhoneNumber, request.Email, cancellationToken);
        if (isExists)
        {
            _logger.LogInformation("Phone number or e-mail address is already registered.");
            return Response<int>.Fail(ErrorCodes.CUSTOMER_EXISTS);
        }

        var customer = ObjectMapper.Mapper.Map<Customer>(request);

        await _uow.CustomerWrite.InsertAsync(customer, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("A new customer with 3 id has been registered.");

        return Response<int>.Success(customer.Id);
    }
}