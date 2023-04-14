namespace Notiflow.Backoffice.Persistence.Repositories.Tenants;

public sealed class TenantReadRepository : ReadRepository<Tenant>, ITenantReadRepository
{
    public TenantReadRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }
}
