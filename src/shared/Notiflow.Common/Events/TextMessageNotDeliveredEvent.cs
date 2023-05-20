namespace Notiflow.Common.Events;

public class TextMessageNotDeliveredEvent
{
    public TextMessageNotDeliveredEvent()
    {
        SentDate = DateTime.Now;
        ErrorMessage = "The message could not be sent for an unknown reason.";
    }

    public string CustomerId { get; set; }
    public string Message { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime SentDate { get; set; }
}