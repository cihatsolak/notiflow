namespace Notiflow.Common.Caching.Models;

public sealed record TenantApplicationCacheModel
{
    public string FirebaseServerKey { get; set; }
    public string FirebaseSenderId { get; set; }
    public string HuaweiServerKey { get; set; }
    public string HuaweiSenderId { get; set; }
    public string MailFromAddress { get; set; }
    public string MailFromName { get; set; }
    public string MailReplyAddress { get; set; }
}
