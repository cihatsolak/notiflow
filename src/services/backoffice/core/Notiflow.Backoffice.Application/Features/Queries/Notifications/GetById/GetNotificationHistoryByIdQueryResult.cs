namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed record GetNotificationHistoryByIdQueryResult
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Message { get; init; }
    public Guid SenderIdentity { get; init; }
    public bool IsSent { get; init; }
    public string ErrorMessage { get; init; }
    public DateTime SentDate { get; init; }
}