namespace Notiflow.IdentityServer.Service.Tenants;

public interface ITenantService
{
    Task<ApiResponse<List<Tenant>>> GetTenantsWithoutFilterAsync(CancellationToken cancellationToken);
}
