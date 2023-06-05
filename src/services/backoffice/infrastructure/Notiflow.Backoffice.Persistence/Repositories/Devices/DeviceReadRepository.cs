namespace Notiflow.Backoffice.Persistence.Repositories.Devices;

public sealed class DeviceReadRepository : ReadRepository<Device>, IDeviceReadRepository
{
    public DeviceReadRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }

    public Task<Device> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
    {
        return Table.SingleOrDefaultAsync(device => device.CustomerId == customerId, cancellationToken);
    }
}
