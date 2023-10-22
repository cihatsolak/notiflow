namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetById;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<GetCustomerByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ValidationErrorCodes> _localizer;

    public GetCustomerByIdQueryHandler(
        INotiflowUnitOfWork notiflowUnitOfWork, 
        ILocalizerService<ValidationErrorCodes> localizer)
    {
        _uow = notiflowUnitOfWork;
        _localizer = localizer;
    }

    public async Task<Result<GetCustomerByIdQueryResult>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<GetCustomerByIdQueryResult>.Failure(StatusCodes.Status404NotFound, _localizer[ValidationErrorCodes.CUSTOMER_NOT_FOUND]);
        }

        var customerDto = ObjectMapper.Mapper.Map<GetCustomerByIdQueryResult>(customer);
        return Result<GetCustomerByIdQueryResult>.Success(StatusCodes.Status200OK, _localizer[ValidationErrorCodes.GENERAL_SUCCESS], customerDto);
    }
}
