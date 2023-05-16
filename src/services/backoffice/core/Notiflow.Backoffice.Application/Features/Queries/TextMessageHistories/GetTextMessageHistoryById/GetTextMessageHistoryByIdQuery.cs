namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetTextMessageHistoryById;

public sealed record GetTextMessageHistoryByIdQuery : IRequest<Response<GetTextMessageHistoryByIdQueryResponse>>
{
    public int Id { get; init; }
}
