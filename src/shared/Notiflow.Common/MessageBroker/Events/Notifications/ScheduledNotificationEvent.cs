namespace Notiflow.Common.MessageBroker.Events.Notifications;

public sealed class ScheduledNotificationEvent
{
    public ScheduledNotificationEvent()
    {
    }

    public ScheduledNotificationEvent(List<int> customerIds, string title, string message, string imageUrl)
    {
        CustomerIds = customerIds;
        Title = title;
        Message = message;
        ImageUrl = imageUrl;
    }

    public List<int> CustomerIds { get; init; }
    public string Title { get; init; }
    public string Message { get; init; }
    public string ImageUrl { get; init; }
}
