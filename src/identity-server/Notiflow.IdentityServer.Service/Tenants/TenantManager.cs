namespace Notiflow.IdentityServer.Service.Tenants;

internal class TenantManager(ApplicationDbContext context) : ITenantService
{
    public async Task<Result<List<Tenant>>> GetTenantsAsync(CancellationToken cancellationToken)
    {
        var tenants = await context.Tenants
                                .TagWith("Lists existing tenants unfiltered.")
                                .IgnoreQueryFilters()
                                .AsNoTracking()
                                .Include(p => p.TenantApplication)
                                .Include(p => p.TenantPermission)
                                .ToListAsync(cancellationToken);

        if (tenants.IsNullOrNotAny())
        {
            return Result<List<Tenant>>.Status404NotFound(ResultCodes.TENANT_NOT_FOUND);
        }

        return Result<List<Tenant>>.Status200OK(ResultCodes.GENERAL_SUCCESS, tenants);
    }
}