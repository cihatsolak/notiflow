﻿namespace Notiflow.Backoffice.Application.Mappers;

internal sealed class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        TenantMaps();
        DeviceMaps();
        TextMessageHistoryMaps();
    }
    
    private void TenantMaps()
    {
        ////Commands
        //CreateMap<AddTenantRequest, Tenant>();

        ////Queries
        //CreateMap<Tenant, GetDetailByIdQueryResponse>();
    }

    private void DeviceMaps()
    {
        CreateMap<AddDeviceCommand, Device>();
        CreateMap<Device, GetDeviceByIdQueryResponse>();
    }

    private void TextMessageHistoryMaps()
    {
        CreateMap<TextMessageHistory, GetTextMessageHistoryByIdQueryResponse>();
    }
}
