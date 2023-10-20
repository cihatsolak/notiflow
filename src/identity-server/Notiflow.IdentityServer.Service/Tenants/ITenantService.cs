namespace Notiflow.IdentityServer.Service.Tenants;

public interface ITenantService
{
    Task<Result<List<Tenant>>> GetTenantsWithoutFilterAsync(CancellationToken cancellationToken);
}
