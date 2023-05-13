namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetCustomerById;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQueryRequest, ResponseData<GetCustomerByIdQueryResponse>>
{
    private readonly INotiflowUnitOfWork _uow;

    public GetCustomerByIdQueryHandler(INotiflowUnitOfWork notiflowUnitOfWork)
    {
        _uow = notiflowUnitOfWork;
    }

    public async Task<ResponseData<GetCustomerByIdQueryResponse>> Handle(GetCustomerByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return ResponseData<GetCustomerByIdQueryResponse>.Fail(ErrorCodes.CUSTOMER_NOT_FOUND);
        }

        return ResponseData<GetCustomerByIdQueryResponse>.Success(SuccessCodes.CUSTOMER_FOUND, ObjectMapper.Mapper.Map<GetCustomerByIdQueryResponse>(customer));
    }
}
