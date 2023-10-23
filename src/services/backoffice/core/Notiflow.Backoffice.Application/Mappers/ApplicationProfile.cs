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
        CreateMap<Customer, CustomerDataTableCommandResult>()
             .ForMember(dest => dest.CloudMessagePlatform, opt => opt.MapFrom(src => src.Device.CloudMessagePlatform));

        CreateMap<Customer, GetCustomerByIdQueryResult>();
    }

    private void DeviceMaps()
    {
        CreateMap<AddDeviceCommand, Device>();
        CreateMap<Device, GetDeviceByIdQueryResult>();

        CreateMap<Device, DeviceDataTableResult>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Customer.Name} {src.Customer.Surname}"));
    }

    private void TextMessageHistoryMaps()
    {
        CreateMap<TextMessageHistory, GetTextMessageHistoryByIdQueryResult>();
        CreateMap<TextMessageHistory, TextMessageDataTableCommandResult>();

        CreateMap<SendTextMessageCommand, TextMessageDeliveredEvent>();
        CreateMap<SendTextMessageCommand, TextMessageNotDeliveredEvent>();
    }

    private void NotificationMaps()
    {
        CreateMap<NotificationHistory, GetNotificationHistoryByIdQueryResult>();
    }

    private void EmailMaps()
    {
        CreateMap<SendEmailCommand, EmailDeliveredEvent>();
        CreateMap<SendEmailCommand, EmailNotDeliveredEvent>();
        CreateMap<SendEmailCommand, EmailRequest>();

        CreateMap<EmailHistory, GetEmailHistoryByIdQueryResult>();
    }
}
