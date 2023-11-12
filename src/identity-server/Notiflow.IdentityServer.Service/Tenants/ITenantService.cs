namespace Notiflow.IdentityServer.Service.Tenants;

public interface ITenantService
{
    Task<Result<List<Tenant>>> GetTenantsAsync(CancellationToken cancellationToken);
}
