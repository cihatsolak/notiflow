namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetById;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Response<GetCustomerByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<GetCustomerByIdQueryHandler> _logger;

    public GetCustomerByIdQueryHandler(
        INotiflowUnitOfWork notiflowUnitOfWork, 
        ILogger<GetCustomerByIdQueryHandler> logger)
    {
        _uow = notiflowUnitOfWork;
        _logger = logger;
    }

    public async Task<Response<GetCustomerByIdQueryResult>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            _logger.LogInformation("Customer with ID {@customerId} was not found.", request.Id);
            return Response<GetCustomerByIdQueryResult>.Fail(ResponseCodes.Error.CUSTOMER_NOT_FOUND);
        }

        var customerDto = ObjectMapper.Mapper.Map<GetCustomerByIdQueryResult>(customer);
        return Response<GetCustomerByIdQueryResult>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, customerDto);
    }
}
