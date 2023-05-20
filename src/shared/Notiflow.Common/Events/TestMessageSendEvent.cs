namespace Notiflow.Common.Events;

public class TestMessageSendEvent
{
    public string CustomerId { get; set; }
    public string Message { get; set; }
    public bool IsSent { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime SentDate { get; set; }
}
