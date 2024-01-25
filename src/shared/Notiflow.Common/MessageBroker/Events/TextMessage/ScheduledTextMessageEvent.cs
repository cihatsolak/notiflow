namespace Notiflow.Common.MessageBroker.Events.TextMessage;

public sealed record ScheduledTextMessageEvent
{
    public ScheduledTextMessageEvent()
    {
    }

    public ScheduledTextMessageEvent(List<int> customerIds, string message)
    {
        CustomerIds = customerIds;
        Message = message;
    }

    public List<int> CustomerIds { get; init; }
    public string Message { get; init; }
}