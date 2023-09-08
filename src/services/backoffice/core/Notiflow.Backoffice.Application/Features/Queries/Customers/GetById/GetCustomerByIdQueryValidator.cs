namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetById;

public sealed class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator()
    {
        RuleFor(p => p.Id).Id(FluenValidationErrorCodes.INVALID_ID_NUMBER);
    }
}
