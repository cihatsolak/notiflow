namespace Notiflow.Backoffice.Application.Models.Emails;

public sealed record EmailRequest
{
    public string Body { get; init; }
    public string Subject { get; init; }
    public List<string> Recipients { get; set; }
    public List<string> CcAddresses { get; init; }
    public List<string> BccAddresses { get; init; }
    public bool IsBodyHtml { get; init; }
}
