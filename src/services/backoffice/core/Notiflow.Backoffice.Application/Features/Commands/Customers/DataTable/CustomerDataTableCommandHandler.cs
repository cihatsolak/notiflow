namespace Notiflow.Backoffice.Application.Features.Commands.Customers.DataTable;

public sealed record CustomerDataTableCommand : DtParameters, IRequest<Result<DtResult<CustomerDataTableCommandResult>>>;

public sealed class CustomerDataTableCommandHandler : IRequestHandler<CustomerDataTableCommand, Result<DtResult<CustomerDataTableCommandResult>>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultMessage> _localizer;

    public CustomerDataTableCommandHandler(
        INotiflowUnitOfWork uow,
        ILocalizerService<ResultMessage> localizer)
    {
        _uow = uow;
        _localizer = localizer;
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
            return Result<DtResult<CustomerDataTableCommandResult>>.Failure(StatusCodes.Status404NotFound, _localizer[ResultMessage.CUSTOMER_NOT_FOUND]);
        }

        DtResult<CustomerDataTableCommandResult> customerDataTable = new()
        {
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Draw = request.Draw,
            Data = ObjectMapper.Mapper.Map<List<CustomerDataTableCommandResult>>(customers)
        };

        return Result<DtResult<CustomerDataTableCommandResult>>.Success(StatusCodes.Status200OK, _localizer[ResultMessage.GENERAL_SUCCESS], customerDataTable);
    }
}