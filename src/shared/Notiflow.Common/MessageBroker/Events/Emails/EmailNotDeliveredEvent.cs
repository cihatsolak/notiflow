namespace Notiflow.Common.MessageBroker.Events.Emails;

public sealed record EmailNotDeliveredEvent
{
    public EmailNotDeliveredEvent()
    {
        SentDate = DateTime.Now;
        ErrorMessage = "The email could not be sent for an unknown reason.";
    }

    public List<int> CustomerIds { get; set; }
    public string Body { get; set; }
    public string Subject { get; set; }
    public List<string> CcAddresses { get; set; }
    public List<string> BccAddresses { get; set; }
    public DateTime SentDate { get; set; }
    public string ErrorMessage { get; set; }
}
