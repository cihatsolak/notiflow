namespace Notiflow.Common.MessageBroker.Events.TextMessage;

public sealed record TextMessageNotDeliveredEvent
{
    public TextMessageNotDeliveredEvent()
    {
        SentDate = DateTime.Now;
        ErrorMessage = "The message could not be sent for an unknown reason.";
    }

    public List<int> CustomerIds { get; init; }
    public string Message { get; init; }
    public string ErrorMessage { get; init; }
    public DateTime SentDate { get; init; }
}