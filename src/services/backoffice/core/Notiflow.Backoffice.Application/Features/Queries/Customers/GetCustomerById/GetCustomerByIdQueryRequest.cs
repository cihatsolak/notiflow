namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetCustomerById;

public sealed record GetCustomerByIdQueryRequest : IRequest<ResponseModel<GetCustomerByIdQueryResponse>>
{
    public int Id { get; init; }
}

