namespace Notiflow.IdentityServer.Service.Observers;

public interface ITenantObserver
{
    Task ExecuteAsync(Guid tenantToken);
}
