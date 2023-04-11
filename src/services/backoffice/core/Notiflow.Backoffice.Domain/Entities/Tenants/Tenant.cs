namespace Notiflow.Backoffice.Domain.Entities.Tenants
{
    public sealed class Tenant : BaseHistoricalEntity
    {
        public string Name { get; set; }
        public string Definition { get; set; }
        public string AppId { get; set; }

        public TenantApplication TenantApplication { get; set; }
        public TenantPermission TenantPermission { get; set; }
    }
}
