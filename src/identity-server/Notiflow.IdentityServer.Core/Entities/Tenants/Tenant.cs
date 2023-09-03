namespace Notiflow.IdentityServer.Core.Entities.Tenants;

public class Tenant : BaseHistoricalEntity<int>
{
    public string Name { get; set; }
    public string Definition { get; set; }
    public Guid Token { get; set; }

    public TenantApplication TenantApplication { get; set; }
    public TenantPermission TenantPermission { get; set; }

    public ICollection<User> Users { get; set; }
}