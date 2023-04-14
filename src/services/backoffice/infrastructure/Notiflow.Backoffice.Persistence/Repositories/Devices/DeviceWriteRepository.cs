namespace Notiflow.Backoffice.Persistence.Repositories.Devices;

public sealed class DeviceWriteRepository : WriteRepository<Device>, IDeviceWriteRepository
{
    public DeviceWriteRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }
}
