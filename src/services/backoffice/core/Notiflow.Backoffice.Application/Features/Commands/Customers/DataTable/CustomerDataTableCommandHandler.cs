namespace Notiflow.Backoffice.Application.Features.Commands.Customers.DataTable;

public sealed class CustomerDataTableCommandHandler : IRequestHandler<CustomerDataTableCommand, Response<DtResult<CustomerDataTableCommandResponse>>>
{
    private readonly INotiflowUnitOfWork _uow;

    public CustomerDataTableCommandHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Response<DtResult<CustomerDataTableCommandResponse>>> Handle(CustomerDataTableCommand request, CancellationToken cancellationToken)
    {
        (int recordsTotal, List<Customer> customers) = await _uow.CustomerRead.GetPageAsync(request.SortKey,
                                                                                            request.SearchKey,
                                                                                            request.PageIndex,
                                                                                            request.PageSize,
                                                                                            cancellationToken
                                                                                            );

        if (customers.IsNullOrNotAny())
        {
            return Response<DtResult<CustomerDataTableCommandResponse>>.Fail(ResponseCodes.Error.CUSTOMER_NOT_FOUND);
        }

        DtResult<CustomerDataTableCommandResponse> customerDataTable = new()
        {
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Draw = request.Draw,
            Data = ObjectMapper.Mapper.Map<List<CustomerDataTableCommandResponse>>(customers)
        };

        return Response<DtResult<CustomerDataTableCommandResponse>>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, customerDataTable);
    }
}