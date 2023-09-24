namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetById;

public sealed record GetTextMessageHistoryByIdQuery : IRequest<Response<GetTextMessageHistoryByIdQueryResult>>
{
    public required int Id { get; init; }
}
