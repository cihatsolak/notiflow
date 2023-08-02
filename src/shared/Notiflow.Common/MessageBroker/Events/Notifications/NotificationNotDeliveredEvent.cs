namespace Notiflow.Common.MessageBroker.Events.Notifications;

public sealed record NotificationNotDeliveredEvent
{
    public NotificationNotDeliveredEvent()
    {
        SentDate = DateTime.Now;
        ErrorMessage = "The notification could not be sent for an unknown reason.";
    }

    public required int CustomerId { get; init; }
    public required string Title { get; init; }
    public required string Message { get; init; }
    public required DateTime SentDate { get; init; }
    public required string ErrorMessage { get; set; }
    public required Guid SenderIdentity { get; set; }
}
