namespace Notiflow.Backoffice.Domain.Entities.Tenants;

public sealed class Tenant : BaseHistoricalEntity
{
    public string Name { get; set; }
    public string Definition { get; set; }
    public Guid AppId { get; set; }

    public TenantApplication TenantApplication { get; set; }
    public TenantPermission TenantPermission { get; set; }

    public ICollection<User> Users { get; set; }
    public ICollection<Customer> Customers { get; set; }
}