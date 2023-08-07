namespace Notiflow.Backoffice.Application.Models.TextMessages;

public sealed record TextMessageRequest
{
    public IEnumerable<string> PhoneNumbers { get; set; }
    public string Message { get; set; }
}
