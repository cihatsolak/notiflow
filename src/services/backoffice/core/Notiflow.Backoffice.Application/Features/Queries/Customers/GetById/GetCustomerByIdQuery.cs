namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetById;

public sealed record GetCustomerByIdQuery : IRequest<ApiResponse<GetCustomerByIdQueryResult>>
{
    public required int Id { get; init; }
}

