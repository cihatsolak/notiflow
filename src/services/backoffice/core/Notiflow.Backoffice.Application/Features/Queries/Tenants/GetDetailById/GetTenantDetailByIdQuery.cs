namespace Notiflow.Backoffice.Application.Features.Queries.Tenants.GetDetailById;

public sealed record GetTenantDetailByIdQuery : IRequest<Response<GetTenantDetailByIdQueryResponse>>
{
    public required int Id { get; init; }
}