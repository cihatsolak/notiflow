namespace Notiflow.IdentityServer.Core.Entities.Tenants;

public class TenantPermission : BaseHistoricalEntity<int>
{
    public bool IsSendMessagePermission { get; set; }
    public bool IsSendNotificationPermission { get; set; }
    public bool IsSendEmailPermission { get; set; }

    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }
}
