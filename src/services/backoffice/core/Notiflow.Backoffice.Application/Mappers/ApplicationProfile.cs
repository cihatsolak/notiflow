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
        //Commands
        CreateMap<AddTenantRequest, Tenant>();

        //Queries
        CreateMap<Tenant, GetDetailByIdQueryResponse>();
    }

    private void DeviceMaps()
    {
        CreateMap<InsertDeviceRequest, Device>();
    }
}
