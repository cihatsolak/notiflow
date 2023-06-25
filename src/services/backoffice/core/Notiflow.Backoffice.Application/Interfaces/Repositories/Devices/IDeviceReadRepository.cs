namespace Notiflow.Backoffice.Application.Interfaces.Repositories.Devices;

public interface IDeviceReadRepository : IReadRepository<Device>
{
    Task<Device> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken);
}
