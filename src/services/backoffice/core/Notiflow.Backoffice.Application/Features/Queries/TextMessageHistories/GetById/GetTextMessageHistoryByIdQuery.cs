namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetById;

public sealed record GetTextMessageHistoryByIdQuery : IRequest<Result<GetTextMessageHistoryByIdQueryResult>>
{
    public int Id { get; init; }

    public GetTextMessageHistoryByIdQuery(int id)
    {
        Id = id;
    }
}
