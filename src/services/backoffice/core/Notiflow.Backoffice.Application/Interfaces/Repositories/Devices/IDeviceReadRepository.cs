namespace Notiflow.Backoffice.Application.Interfaces.Repositories.Devices;

public interface IDeviceReadRepository : IReadRepository<Device>
{
    Task<(int recordsTotal, List<Device> devices)> GetPageAsync(string sortKey, string searchKey, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<Device> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken);
    Task<Device> GetCloudMessagePlatformByCustomerIdAsync(int customerId, CancellationToken cancellationToken);
    Task<List<Device>> GetCloudMessagePlatformByCustomerIdsAsync(List<int> customerIds, CancellationToken cancellationToken);
}
