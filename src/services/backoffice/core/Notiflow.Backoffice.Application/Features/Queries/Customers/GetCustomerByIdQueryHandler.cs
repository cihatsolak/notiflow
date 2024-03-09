namespace Notiflow.Backoffice.Application.Features.Queries.Customers;

public sealed record GetCustomerByIdQuery(int Id) : IRequest<Result<GetCustomerByIdQueryResult>>;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<GetCustomerByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;

    public GetCustomerByIdQueryHandler(INotiflowUnitOfWork notiflowUnitOfWork)
    {
        _uow = notiflowUnitOfWork;
    }

    public async Task<Result<GetCustomerByIdQueryResult>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<GetCustomerByIdQueryResult>.Status404NotFound(ResultCodes.CUSTOMER_NOT_FOUND);
        }

        var customerDto = ObjectMapper.Mapper.Map<GetCustomerByIdQueryResult>(customer);
        return Result<GetCustomerByIdQueryResult>.Status200OK(ResultCodes.GENERAL_SUCCESS, customerDto);
    }
}

public sealed class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}
public sealed record GetCustomerByIdQueryResult
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public string PhoneNumber { get; init; }
    public string Email { get; init; }
    public DateTime BirthDate { get; init; }
    public Gender Gender { get; init; }
    public MarriageStatus MarriageStatus { get; init; }
    public bool IsBlocked { get; init; }
}
