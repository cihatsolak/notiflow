namespace Notiflow.Backoffice.Application.Features.Queries.Customers;

public sealed record GetCustomerByIdQuery(int Id) : IRequest<Result<CustomerResponse>>;

public sealed class GetCustomerByIdQueryHandler(INotiflowUnitOfWork notiflowUnitOfWork) 
    : IRequestHandler<GetCustomerByIdQuery, Result<CustomerResponse>>
{
    public async Task<Result<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await notiflowUnitOfWork.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<CustomerResponse>.Status404NotFound(ResultCodes.CUSTOMER_NOT_FOUND);
        }

        return Result<CustomerResponse>.Status200OK(ResultCodes.GENERAL_SUCCESS, ObjectMapper.Mapper.Map<CustomerResponse>(customer));
    }
}

public sealed class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}

public sealed record CustomerResponse
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
