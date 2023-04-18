using Notiflow.Backoffice.Application.Features.Commands.Devices.Insert;
using Notiflow.Backoffice.Application.Features.Commands.Tenants.Add;
using Notiflow.Backoffice.Application.Features.Queries.Tenants.GetDetailById;

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
