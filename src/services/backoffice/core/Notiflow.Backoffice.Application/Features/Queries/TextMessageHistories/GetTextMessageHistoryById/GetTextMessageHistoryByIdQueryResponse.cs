namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetTextMessageHistoryById;

public sealed record GetTextMessageHistoryByIdQueryResponse
{
    public int Id { get; init; }
    public string Message { get; init; }
    public bool IsSent { get; init; }
    public string ErrorMessage { get; init; }
    public DateTime SentDate { get; init; }
}