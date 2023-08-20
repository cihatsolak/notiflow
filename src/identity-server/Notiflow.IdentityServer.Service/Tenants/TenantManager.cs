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

    public async Task<Response<List<Tenant>>> GetTenantsWithoutFilter(CancellationToken cancellationToken)
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
            return Response<List<Tenant>>.Fail(-1);
        }

        return Response<List<Tenant>>.Success(tenants);
    }
}
