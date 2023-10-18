namespace Notiflow.Common.MessageBroker.Events.Notifications;

public sealed class ScheduledNotificationEvent
{
    public required List<int> CustomerIds { get; init; }
    public required string Title { get; init; }
    public required string Message { get; init; }
    public required string ImageUrl { get; init; }
}
