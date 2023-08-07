namespace Notiflow.Backoffice.Application.Models.TextMessages;

public sealed record TextMessageResult
{
    public string PhoneNumber { get; set; }
    public bool IsSent { get; set; }
}
