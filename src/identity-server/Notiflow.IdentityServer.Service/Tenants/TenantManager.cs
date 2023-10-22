namespace Notiflow.IdentityServer.Service.Tenants;

internal class TenantManager : ITenantService
{
    private readonly ApplicationDbContext _context;
    private readonly ILocalizerService<ResultState> _localizer;

    public TenantManager(
       ApplicationDbContext context,
       ILocalizerService<ResultState> localizer)
    {
        _context = context;
        _localizer = localizer;
    }

    public async Task<Result<List<Tenant>>> GetTenantsWithoutFilterAsync(CancellationToken cancellationToken)
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
            return Result<List<Tenant>>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.TENANT_NOT_FOUND]);
        }

        return Result<List<Tenant>>.Success(StatusCodes.Status200OK, _localizer[ResultState.GENERAL_SUCCESS], tenants);
    }
}