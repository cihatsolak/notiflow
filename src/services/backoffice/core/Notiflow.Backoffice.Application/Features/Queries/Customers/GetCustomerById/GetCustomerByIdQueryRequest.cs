namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetCustomerById;

public sealed record GetCustomerByIdQueryRequest : IRequest<Response<GetCustomerByIdQueryResponse>>
{
    public int Id { get; init; }
}

