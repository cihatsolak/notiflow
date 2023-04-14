namespace Notiflow.Backoffice.Persistence.Repositories.Tenants;

public sealed class TenantWriteRepository : WriteRepository<Tenant>, ITenantWriteRepository
{
    public TenantWriteRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }
}
