namespace Notiflow.Backoffice.Application.Features.Commands.Customers.DataTable;

public sealed class CustomerDataTableCommandHandler : IRequestHandler<CustomerDataTableCommand, ApiResponse<DtResult<CustomerDataTableCommandResult>>>
{
    private readonly INotiflowUnitOfWork _uow;

    public CustomerDataTableCommandHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<DtResult<CustomerDataTableCommandResult>>> Handle(CustomerDataTableCommand request, CancellationToken cancellationToken)
    {
        (int recordsTotal, List<Customer> customers) = await _uow.CustomerRead.GetPageAsync(request.SortKey,
                                                                                            request.SearchKey,
                                                                                            request.PageIndex,
                                                                                            request.PageSize,
                                                                                            cancellationToken
                                                                                            );

        if (customers.IsNullOrNotAny())
        {
            return ApiResponse<DtResult<CustomerDataTableCommandResult>>.Fail(ResponseCodes.Error.CUSTOMER_NOT_FOUND);
        }

        DtResult<CustomerDataTableCommandResult> customerDataTable = new()
        {
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Draw = request.Draw,
            Data = ObjectMapper.Mapper.Map<List<CustomerDataTableCommandResult>>(customers)
        };

        return ApiResponse<DtResult<CustomerDataTableCommandResult>>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, customerDataTable);
    }
}