namespace Notiflow.Backoffice.Application.Features.Queries.Tenants.GetDetailById;

public sealed record GetDetailByIdQueryRequest : IRequest<ResponseData<GetDetailByIdQueryResponse>>
{
    public int Id { get; init; }
}