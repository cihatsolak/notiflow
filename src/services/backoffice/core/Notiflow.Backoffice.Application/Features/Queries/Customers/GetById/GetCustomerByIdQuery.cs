namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetById;

public sealed record GetCustomerByIdQuery : IRequest<Result<GetCustomerByIdQueryResult>>
{
    public int Id { get; init; }

    public GetCustomerByIdQuery(int id)
    {
        Id = id;
    }
}

