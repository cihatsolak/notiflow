namespace Notiflow.Backoffice.Domain.Entities.Tenants
{
    public sealed class TenantApplication : BaseHistoricalEntity
    {
        public string FirebaseServerKey { get; set; }
        public string FirebaseSenderId { get; set; }
        public string HuaweiServerKey { get; set; }
        public string HuaweiSenderId { get; set; }
        public string MailFromAddress { get; set; }
        public string MailFromName { get; set; }
        public string MailReplyAddress { get; set; }

        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
