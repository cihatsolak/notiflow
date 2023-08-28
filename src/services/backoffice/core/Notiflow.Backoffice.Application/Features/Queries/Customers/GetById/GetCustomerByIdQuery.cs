namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetById;

public sealed record GetCustomerByIdQuery : IRequest<Response<GetCustomerByIdQueryResponse>>
{
    public required int Id { get; init; }
}

