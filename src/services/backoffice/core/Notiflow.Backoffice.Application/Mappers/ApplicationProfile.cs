namespace Notiflow.Backoffice.Application.Mappers;

internal sealed class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        TenantMaps();
        DeviceMaps();
    }
    
    private void TenantMaps()
    {
        CreateMap<AddTenantRequest, Tenant>();
    }

    private void DeviceMaps()
    {
        CreateMap<InsertDeviceRequest, Device>();
    }
}
