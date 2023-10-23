namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetById;

public sealed record GetTextMessageHistoryByIdQuery : IRequest<Result<GetTextMessageHistoryByIdQueryResult>>
{
    public required int Id { get; init; }
}
