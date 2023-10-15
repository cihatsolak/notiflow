namespace Notiflow.IdentityServer.Service.Tenants;

internal class TenantManager : ITenantService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TenantManager> _logger;

    public TenantManager(
       ApplicationDbContext context,
        ILogger<TenantManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApiResponse<List<Tenant>>> GetTenantsWithoutFilterAsync(CancellationToken cancellationToken)
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
            _logger.LogWarning("The tenants information could not be found.");
            return ApiResponse<List<Tenant>>.Failure(-1);
        }

        return ApiResponse<List<Tenant>>.Success(tenants);
    }
}
