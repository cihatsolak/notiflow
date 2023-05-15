namespace Notiflow.Backoffice.Application.Features.Queries.TextMessages.GetTextMessageDetailById;

public sealed record GetTextMessageDetailByIdQuery : IRequest<Response<GetTextMessageDetailByIdQueryResponse>>
{
    public int Id { get; init; }
}
