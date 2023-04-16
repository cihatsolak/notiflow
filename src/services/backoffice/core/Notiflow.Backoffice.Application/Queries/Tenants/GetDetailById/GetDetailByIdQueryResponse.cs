namespace Notiflow.Backoffice.Application.Queries.Tenants.GetDetailById;

public sealed record GetDetailByIdQueryResponse
{
    public string Name { get; init; }
    public string Definition { get; init; }
    public Guid AppId { get; init; }
}
