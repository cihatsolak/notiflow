namespace Notiflow.IdentityServer.Service.Tenants;

public interface ITenantService
{
    Guid TenantToken { get; }
}
