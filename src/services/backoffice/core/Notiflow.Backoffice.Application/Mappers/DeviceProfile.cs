namespace Notiflow.Backoffice.Application.Mappers;

internal sealed class DeviceProfile : Profile
{
    public DeviceProfile()
    {
        CreateMap<InsertDeviceRequest, Device>();
    }
}
