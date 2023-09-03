namespace Notiflow.IdentityServer.Core.Entities.Tenants;

public class TenantApplication : BaseHistoricalEntity<int>
{
    public string FirebaseServerKey { get; set; }
    public string FirebaseSenderId { get; set; }
    public string HuaweiServerKey { get; set; }
    public string HuaweiSenderId { get; set; }
    public string MailFromAddress { get; set; }
    public string MailFromName { get; set; }
    public string MailReplyAddress { get; set; }
    public string MailSmtpHost { get; set; }
    public int MailSmtpPort { get; set; }

    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }
}
