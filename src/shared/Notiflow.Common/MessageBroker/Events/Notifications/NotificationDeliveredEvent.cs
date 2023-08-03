namespace Notiflow.Common.MessageBroker.Events.Notifications;

public sealed record NotificationDeliveredEvent
{
    public NotificationDeliveredEvent()
    {
        SentDate = DateTime.Now;
    }

    public required int CustomerId { get; init; }
    public required string Title { get; init; }
    public required string Message { get; init; }
    public required DateTime SentDate { get; init; }
    public required Guid SenderIdentity { get; set; }
}
