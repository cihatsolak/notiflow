namespace Notiflow.IdentityServer.Service.Observers;

public interface ITenantObserverSubject
{
    Task NotifyObserversAsync(Guid tenantToken);
}

internal sealed class TenantObserverSubject : ITenantObserverSubject
{
    private readonly List<ITenantObserver> _tenantObservers;

    public TenantObserverSubject()
    {
        _tenantObservers = [];
    }

    public void RegisterObserver(ITenantObserver tenantObserver)
    {
        _tenantObservers.Add(tenantObserver);
    }

    public void RemoveObserver(ITenantObserver tenantObserver)
    {
        _tenantObservers.Remove(tenantObserver);
    }

    public async Task NotifyObserversAsync(Guid tenantToken)
    {
        var tasks = _tenantObservers.Select(observer => observer.ExecuteAsync(tenantToken));
        await Task.WhenAll(tasks);
    }
}

