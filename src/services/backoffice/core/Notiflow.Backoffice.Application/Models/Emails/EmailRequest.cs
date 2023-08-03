namespace Notiflow.Backoffice.Application.Models.Emails;

public class EmailRequest
{
    public string Body { get; set; }
    public string Subject { get; set; }
    public List<string> ToAddresses { get; set; }
    public List<string> CcAddresses { get; set; }
    public List<string> BccAddresses { get; set; }
}
