namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetById;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<GetCustomerByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultState> _localizer;

    public GetCustomerByIdQueryHandler(
        INotiflowUnitOfWork notiflowUnitOfWork, 
        ILocalizerService<ResultState> localizer)
    {
        _uow = notiflowUnitOfWork;
        _localizer = localizer;
    }

    public async Task<Result<GetCustomerByIdQueryResult>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<GetCustomerByIdQueryResult>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.CUSTOMER_NOT_FOUND]);
        }

        var customerDto = ObjectMapper.Mapper.Map<GetCustomerByIdQueryResult>(customer);
        return Result<GetCustomerByIdQueryResult>.Success(StatusCodes.Status200OK, _localizer[ResultState.GENERAL_SUCCESS], customerDto);
    }
}
