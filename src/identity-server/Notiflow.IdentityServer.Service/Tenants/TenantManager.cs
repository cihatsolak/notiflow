namespace Notiflow.IdentityServer.Service.Tenants;

internal class TenantManager : ITenantService
{
    private readonly ApplicationDbContext _context;

    public TenantManager(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Tenant>>> GetTenantsAsync(CancellationToken cancellationToken)
    {
        var tenants = await  _context.Tenants
                                .TagWith("Lists existing tenants unfiltered.")
                                .IgnoreQueryFilters()
                                .AsNoTracking()
                                .Include(p => p.TenantApplication)
                                .Include(p => p.TenantPermission)
                                .ToListAsync(cancellationToken);

        if (tenants.IsNullOrNotAny())
        {
            return Result<List<Tenant>>.Failure(StatusCodes.Status404NotFound, ResultCodes.TENANT_NOT_FOUND);
        }

        return Result<List<Tenant>>.Success(StatusCodes.Status200OK, ResultCodes.GENERAL_SUCCESS, tenants);
    }
}