namespace Notiflow.Backoffice.Application.Features.Queries.Tenants.GetDetailById;

public sealed record GetDetailByIdQueryRequest : IRequest<Response<GetDetailByIdQueryResponse>>
{
    public int Id { get; init; }
}