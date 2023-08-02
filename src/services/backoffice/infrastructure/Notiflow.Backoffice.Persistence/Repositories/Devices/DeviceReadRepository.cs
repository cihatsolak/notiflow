namespace Notiflow.Backoffice.Persistence.Repositories.Devices;

public sealed class DeviceReadRepository : ReadRepository<Device>, IDeviceReadRepository
{
    public DeviceReadRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }

    public Task<Device> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
    {
        return Table
               .TagWith("Queries the customer's device information.")
               .SingleOrDefaultAsync(device => device.CustomerId == customerId, cancellationToken);
    }

    public Task<Device> GetCloudMessagePlatformByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
    {
        return TableNoTracking
               .TagWith("Returns the customer's notification platform and the platform's token information.")
               .Where(device => device.CustomerId == customerId)
               .Select(device => new Device
               {
                   CloudMessagePlatform = device.CloudMessagePlatform,
                   Token = device.Token
               })
               .SingleOrDefaultAsync(cancellationToken);
    }

    public Task<List<Device>> GetCloudMessagePlatformByCustomerIdsAsync(List<int> customerIds, CancellationToken cancellationToken)
    {
        return TableNoTracking
               .TagWith("Returns the customer's notification platform and the platform's token information.")
               .Where(device => customerIds.Any(customerId => customerId == device.CustomerId))
               .Select(device => new Device
               {
                   CloudMessagePlatform = device.CloudMessagePlatform,
                   Token = device.Token
               })
              .ToListAsync(cancellationToken);
    }
}
