using Notiflow.Common.Localize;

namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetById;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ApiResponse<GetCustomerByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;

    public GetCustomerByIdQueryHandler(
        INotiflowUnitOfWork notiflowUnitOfWork)
    {
        _uow = notiflowUnitOfWork;
    }

    public async Task<ApiResponse<GetCustomerByIdQueryResult>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return ApiResponse<GetCustomerByIdQueryResult>.Failure(ResponseCodes.Error.CUSTOMER_NOT_FOUND);
        }

        var customerDto = ObjectMapper.Mapper.Map<GetCustomerByIdQueryResult>(customer);
        return ApiResponse<GetCustomerByIdQueryResult>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, customerDto);
    }
}
