namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetTextMessageHistoryById;

public sealed record GetTextMessageHistoryByIdQuery : IRequest<Response<GetTextMessageHistoryByIdQueryResponse>>
{
    public required int Id { get; init; }
}
