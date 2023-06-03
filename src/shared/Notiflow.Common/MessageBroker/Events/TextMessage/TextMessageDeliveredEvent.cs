namespace Notiflow.Common.MessageBroker.Events.TextMessage;

public class TextMessageDeliveredEvent
{
    public TextMessageDeliveredEvent()
    {
        SentDate = DateTime.Now;
    }

    public int CustomerId { get; set; }
    public string Message { get; set; }
    public DateTime SentDate { get; set; }
}
