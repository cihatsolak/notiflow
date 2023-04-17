namespace Notiflow.Backoffice.Application.Queries.Customers.GetCustomerById;

public sealed class GetCustomerByIdQueryValidor : AbstractValidator<GetCustomerByIdQueryRequest>
{
    public GetCustomerByIdQueryValidor()
    {
        RuleFor(p => p.Id).InclusiveBetween(1, int.MaxValue).WithMessage("");
    }
}