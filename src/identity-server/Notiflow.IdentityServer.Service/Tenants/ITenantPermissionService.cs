using Mapster;
using Notiflow.IdentityServer.Service.Models;

namespace Notiflow.IdentityServer.Service.Tenants
{
    public interface ITenantPermissionService
    {
        Task<ResponseData<TenantPermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken);
        Task<ResponseData<int>> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken);
    }

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
                _logger.LogInformation("");
                return ResponseData<TenantPermissionResponse>.Fail(-1);
            }

            return ResponseData<TenantPermissionResponse>.Success(tenantPermission);
        }

        public async Task<ResponseData<int>> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken)
        {
            var tenantPermission = await _context.TenantPermissions.FirstAsync(cancellationToken);
            if (tenantPermission is null)
            {
                _logger.LogInformation("");
                return ResponseData<int>.Fail(-1);
            }

            tenantPermission.IsSendMessagePermission = request.IsSendMessagePermission;
            tenantPermission.IsSendNotificationPermission = request.IsSendNotificationPermission;
            tenantPermission.IsSendEmailPermission = request.IsSendEmailPermission;

            await _context.SaveChangesAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }

}
