namespace Notiflow.Backoffice.Application.Features.Commands.Customers.DataTable;

public sealed record CustomerDataTableCommand : DtParameters, IRequest<Result<DtResult<CustomerDataTableCommandResult>>>;

public sealed class CustomerDataTableCommandHandler : IRequestHandler<CustomerDataTableCommand, Result<DtResult<CustomerDataTableCommandResult>>>
{
    private readonly INotiflowUnitOfWork _uow;

    public CustomerDataTableCommandHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Result<DtResult<CustomerDataTableCommandResult>>> Handle(CustomerDataTableCommand request, CancellationToken cancellationToken)
    {
        (int recordsTotal, List<Customer> customers) = await _uow.CustomerRead.GetPageAsync(request.SortKey,
                                                                                            request.SearchKey,
                                                                                            request.PageIndex,
                                                                                            request.PageSize,
                                                                                            cancellationToken
                                                                                            );

        if (customers.IsNullOrNotAny())
        {
            return Result<DtResult<CustomerDataTableCommandResult>>.Status404NotFound(ResultCodes.CUSTOMER_NOT_FOUND);
        }

        DtResult<CustomerDataTableCommandResult> customerDataTable = new()
        {
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Draw = request.Draw,
            Data = ObjectMapper.Mapper.Map<List<CustomerDataTableCommandResult>>(customers)
        };

        return Result<DtResult<CustomerDataTableCommandResult>>.Status200OK(ResultCodes.GENERAL_SUCCESS, customerDataTable);
    }
}

public sealed record CustomerDataTableCommandResult
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string PhoneNumber { get; init; }
    public string Email { get; init; }
    public bool IsBlocked { get; init; }
    public bool IsDeleted { get; init; }
    public CloudMessagePlatform CloudMessagePlatform { get; init; }
}