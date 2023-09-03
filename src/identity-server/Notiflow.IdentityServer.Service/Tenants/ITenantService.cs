namespace Notiflow.IdentityServer.Service.Tenants;

public interface ITenantService
{
    Task<Response<List<Tenant>>> GetTenantsWithoutFilterAsync(CancellationToken cancellationToken);
}
