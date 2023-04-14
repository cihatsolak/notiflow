namespace Notiflow.Backoffice.Application.Interfaces.UnitOfWorks;

public interface INotiflowUnitOfWork : IBaseUnitOfWork
{
    ICustomerReadRepository CustomerRead { get; }
    ICustomerWriteRepository CustomerWrite { get; }
    ITenantReadRepository TenantRead { get; }
    ITenantWriteRepository TenantWrite { get; }
    IDeviceReadRepository DeviceRead { get; }
    IDeviceWriteRepository DeviceWrite { get; }
}