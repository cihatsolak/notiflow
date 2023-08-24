namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetCustomerById;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Response<GetCustomerByIdQueryResponse>>
{
    private readonly INotiflowUnitOfWork _uow;

    public GetCustomerByIdQueryHandler(INotiflowUnitOfWork notiflowUnitOfWork)
    {
        _uow = notiflowUnitOfWork;
    }

    public async Task<Response<GetCustomerByIdQueryResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Response<GetCustomerByIdQueryResponse>.Fail(ResponseErrorCodes.CUSTOMER_NOT_FOUND);
        }

        return Response<GetCustomerByIdQueryResponse>.Success(ObjectMapper.Mapper.Map<GetCustomerByIdQueryResponse>(customer));
    }
}
