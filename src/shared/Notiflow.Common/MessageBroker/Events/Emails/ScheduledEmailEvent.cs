namespace Notiflow.Common.MessageBroker.Events.Emails;

public sealed record ScheduledEmailEvent
{
    public ScheduledEmailEvent()
    {
    }

    public ScheduledEmailEvent(string body, string subject, List<int> customerIds, List<string> ccAddresses, List<string> bccAddresses, bool isBodyHtml)
    {
        Body = body;
        Subject = subject;
        CustomerIds = customerIds;
        CcAddresses = ccAddresses;
        BccAddresses = bccAddresses;
        IsBodyHtml = isBodyHtml;
    }

    public string Body { get; init; }
    public string Subject { get; init; }
    public List<int> CustomerIds { get; init; }
    public List<string> CcAddresses { get; init; }
    public List<string> BccAddresses { get; init; }
    public bool IsBodyHtml { get; init; }
}
