namespace Notiflow.IdentityServer.Core.Entities.Tenants;

public class Tenant : BaseHistoricalEntity
{
    public string Name { get; set; }
    public string Definition { get; set; }
    public Guid ApplicationId { get; set; }

    public TenantApplication TenantApplication { get; set; }
    public TenantPermission TenantPermission { get; set; }

    public ICollection<User> Users { get; set; }
}