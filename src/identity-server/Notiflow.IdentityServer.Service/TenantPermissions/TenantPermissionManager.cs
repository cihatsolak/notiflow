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

    public async Task<Response<TenantPermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken)
    {
        var tenantPermission = await _context.TenantPermissions.AsNoTracking().ProjectToType<TenantPermissionResponse>().SingleAsync(cancellationToken);
        if (tenantPermission is null)
        {
            _logger.LogInformation("Tenant permissions not found.");
            return Response<TenantPermissionResponse>.Fail(-1);
        }

        return Response<TenantPermissionResponse>.Success(tenantPermission);
    }

    public async Task<Response<EmptyResponse>> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken)
    {
        var tenantPermission = await _context.TenantPermissions.SingleAsync(cancellationToken);
        if (tenantPermission is null)
        {
            _logger.LogInformation("Tenant permissions not found.");
            return Response<EmptyResponse>.Fail(-1);
        }

        tenantPermission.IsSendMessagePermission = request.IsSendMessagePermission;
        tenantPermission.IsSendNotificationPermission = request.IsSendNotificationPermission;
        tenantPermission.IsSendEmailPermission = request.IsSendEmailPermission;

        await _context.SaveChangesAsync(cancellationToken);

        return Response<EmptyResponse>.Success(-1);
    }
}
