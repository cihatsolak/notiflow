namespace Notiflow.Common.Events;

public class TextMessageNotDeliveredEvent
{
    public string CustomerId { get; set; }
    public string Message { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime SentDate { get; set; }
}