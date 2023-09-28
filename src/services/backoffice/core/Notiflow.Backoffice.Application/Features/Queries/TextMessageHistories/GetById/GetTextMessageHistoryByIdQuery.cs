namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetById;

public sealed record GetTextMessageHistoryByIdQuery : IRequest<ApiResponse<GetTextMessageHistoryByIdQueryResult>>
{
    public required int Id { get; init; }
}
