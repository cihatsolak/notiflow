namespace Notiflow.IdentityServer.Service.Tenants;

public interface ITenantService
{
    Task<Response<List<Tenant>>> GetTenantsAsync(CancellationToken cancellationToken);
}
