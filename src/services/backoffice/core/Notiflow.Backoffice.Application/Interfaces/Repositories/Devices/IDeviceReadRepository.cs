namespace Notiflow.Backoffice.Application.Interfaces.Repositories.Devices;

public interface IDeviceReadRepository : IReadRepository<Device>
{
    Task<Device> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken);
    Task<Device> GetCloudMessagePlatformByCustomerIdAsync(int customerId, CancellationToken cancellationToken);
    Task<List<Device>> GetCloudMessagePlatformByCustomerIdsAsync(List<int> customerIds, CancellationToken cancellationToken);
}
