namespace Notiflow.Backoffice.Persistence.Repositories.Devices;

public sealed class DeviceWriteRepository(NotiflowDbContext notiflowDbContext) 
    : WriteRepository<Device>(notiflowDbContext), IDeviceWriteRepository
{
}
