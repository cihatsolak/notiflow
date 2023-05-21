namespace Notiflow.Common.Events.Notifications;

public sealed record NotificationNotDeliveredEvent
{
    public NotificationNotDeliveredEvent()
    {
        SentDate = DateTime.Now;
    }

    public required int CustomerId { get; init; }
    public required string Title { get; init; }
    public required string Message { get; init; }
    public required DateTime SentDate { get; init; }
    public required string ErrorMessage { get; init; }
}
