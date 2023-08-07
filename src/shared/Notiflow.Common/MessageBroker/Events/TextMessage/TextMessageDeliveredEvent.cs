namespace Notiflow.Common.MessageBroker.Events.TextMessage;

public sealed record TextMessageDeliveredEvent
{
    public TextMessageDeliveredEvent()
    {
        SentDate = DateTime.Now;
    }

    public List<int> CustomerIds { get; init; }
    public string Message { get; init; }
    public DateTime SentDate { get; init; }
}
