namespace Notiflow.Backoffice.Domain.Entities.Tenants
{
    public sealed class TenantPermission : BaseHistoricalEntity
    {
        public bool IsSendMessagePermission { get; set; }
        public bool IsSendNotificationPermission { get; set; }
        public bool IsSendEmailPermission { get; set; }

        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
