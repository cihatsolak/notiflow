namespace Notiflow.Backoffice.Application.Queries.Customers.GetCustomerById;

public sealed record GetCustomerByIdQueryRequest : IRequest<ResponseModel<GetCustomerByIdQueryResponse>>
{
    public int Id { get; init; }
}

