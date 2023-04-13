namespace Notiflow.Backoffice.Persistence.Repositories.Devices;

public sealed class DeviceReadRepository : ReadRepository<Device>, IDeviceReadRepository
{
    public DeviceReadRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }
}
