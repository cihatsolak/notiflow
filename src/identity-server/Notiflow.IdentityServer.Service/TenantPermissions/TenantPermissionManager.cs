namespace Notiflow.IdentityServer.Service.TenantPermissions;

internal sealed class TenantPermissionManager : ITenantPermissionService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TenantPermissionManager> _logger;

    public TenantPermissionManager(
        ApplicationDbContext context,
        ILogger<TenantPermissionManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ResponseData<TenantPermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken)
    {
        var tenantPermission = await _context.TenantPermissions.AsNoTracking().ProjectToType<TenantPermissionResponse>().FirstAsync(cancellationToken);
        if (tenantPermission is null)
        {
            _logger.LogInformation("Tenant permissions not found.");
            return ResponseData<TenantPermissionResponse>.Fail(-1);
        }

        return ResponseData<TenantPermissionResponse>.Success(tenantPermission);
    }

    public async Task<Response> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken)
    {
        var tenantPermission = await _context.TenantPermissions.FirstAsync(cancellationToken);
        if (tenantPermission is null)
        {
            _logger.LogInformation("Tenant permissions not found.");
            return Response.Fail(-1);
        }

        tenantPermission.IsSendMessagePermission = request.IsSendMessagePermission;
        tenantPermission.IsSendNotificationPermission = request.IsSendNotificationPermission;
        tenantPermission.IsSendEmailPermission = request.IsSendEmailPermission;

        await _context.SaveChangesAsync(cancellationToken);

        return Response.Success(-1);
    }
}
