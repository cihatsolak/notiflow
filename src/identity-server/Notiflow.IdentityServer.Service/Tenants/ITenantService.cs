namespace Notiflow.IdentityServer.Service.Tenants;

public interface ITenantService
{
    Task<Response<List<Tenant>>> GetTenantsWithoutFilter(CancellationToken cancellationToken);
}
