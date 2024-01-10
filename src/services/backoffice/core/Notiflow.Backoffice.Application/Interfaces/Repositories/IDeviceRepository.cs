namespace Notiflow.Backoffice.Application.Interfaces.Repositories;

public interface IDeviceReadRepository : IReadRepository<Device>
{
    Task<(int recordsTotal, List<Device> devices)> GetPageAsync(string sortKey, string searchKey, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<Device> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken);
    Task<Device> GetCloudMessagePlatformByCustomerIdAsync(int customerId, CancellationToken cancellationToken);
    Task<List<Device>> GetCloudMessagePlatformByCustomerIdsAsync(List<int> customerIds, CancellationToken cancellationToken);
}

public interface IDeviceWriteRepository : IWriteRepository<Device>
{
}
