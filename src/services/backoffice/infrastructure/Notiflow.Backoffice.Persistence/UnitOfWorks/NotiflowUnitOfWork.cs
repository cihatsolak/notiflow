namespace Notiflow.Backoffice.Persistence.UnitOfWorks;

internal sealed class NotiflowUnitOfWork : BaseUnitOfWork, INotiflowUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;

    public NotiflowUnitOfWork(
        NotiflowDbContext context,
        IServiceProvider serviceProvider) : base(context)
    {
        _serviceProvider = serviceProvider;
    }

    public ICustomerReadRepository CustomerRead => _serviceProvider.GetRequiredService<ICustomerReadRepository>();

    public ICustomerWriteRepository CustomerWrite => _serviceProvider.GetRequiredService<ICustomerWriteRepository>();

    public ITenantReadRepository TenantRead => _serviceProvider.GetRequiredService<ITenantReadRepository>();

    public ITenantWriteRepository TenantWrite => _serviceProvider.GetRequiredService<ITenantWriteRepository>();

    public IDeviceReadRepository DeviceRead => _serviceProvider.GetRequiredService<IDeviceReadRepository>();

    public IDeviceWriteRepository DeviceWrite => _serviceProvider.GetRequiredService<IDeviceWriteRepository>();
}
