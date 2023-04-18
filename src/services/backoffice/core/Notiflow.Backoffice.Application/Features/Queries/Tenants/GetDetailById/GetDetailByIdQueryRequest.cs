namespace Notiflow.Backoffice.Application.Features.Queries.Tenants.GetDetailById;

public sealed record GetDetailByIdQueryRequest : IRequest<ResponseModel<GetDetailByIdQueryResponse>>
{
    public int Id { get; init; }
}