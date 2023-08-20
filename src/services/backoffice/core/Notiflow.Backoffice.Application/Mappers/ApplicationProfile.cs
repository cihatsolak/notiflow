namespace Notiflow.Backoffice.Application.Mappers;

internal sealed class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        DeviceMaps();
        TextMessageHistoryMaps();
        NotificationMaps();
        EmailMaps();
        CustomerMaps();
    }

    private void CustomerMaps()
    {
        CreateMap<Customer, CustomerDataTableResponse>()
             .ForMember(dest => dest.CloudMessagePlatform, opt => opt.MapFrom(src => src.Device.CloudMessagePlatform));

        CreateMap<Customer, GetCustomerByIdQueryResponse>();
    }

    private void DeviceMaps()
    {
        CreateMap<AddDeviceCommand, Device>();
        CreateMap<Device, GetDeviceByIdQueryResponse>();

        CreateMap<Device, DeviceDataTableResponse>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Customer.Name} {src.Customer.Surname}"));
    }

    private void TextMessageHistoryMaps()
    {
        CreateMap<TextMessageHistory, GetTextMessageHistoryByIdQueryResponse>();

        CreateMap<SendTextMessageCommand, TextMessageDeliveredEvent>();
        CreateMap<SendTextMessageCommand, TextMessageNotDeliveredEvent>();
    }

    private void NotificationMaps()
    {
        CreateMap<SendSingleNotificationCommand, NotificationDeliveredEvent>();
        CreateMap<SendSingleNotificationCommand, NotificationNotDeliveredEvent>();
    }

    private void EmailMaps()
    {
        CreateMap<SendEmailCommand, EmailDeliveredEvent>();
        CreateMap<SendEmailCommand, EmailNotDeliveredEvent>();
        CreateMap<SendEmailCommand, EmailRequest>();
    }
}
