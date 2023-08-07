namespace Notiflow.Common.MessageBroker.Events.Emails;

public sealed record EmailDeliveredEvent
{
    public EmailDeliveredEvent()
    {
        SentDate = DateTime.Now;
    }

    public List<int> CustomerIds { get; set; }
    public string Body { get; set; }
    public string Subject { get; set; }
    public List<string> Recipients { get; set; }
    public List<string> CcAddresses { get; set; }
    public List<string> BccAddresses { get; set; }
    public DateTime SentDate { get; init; }
    public bool IsBodyHtml { get; init; }
}
