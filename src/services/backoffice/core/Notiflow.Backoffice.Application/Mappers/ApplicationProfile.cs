namespace Notiflow.Backoffice.Application.Mappers;

internal sealed class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        DeviceMaps();
        TextMessageHistoryMaps();
        NotificationMaps();
        EmailMaps();
    }

    private void DeviceMaps()
    {
        CreateMap<AddDeviceCommand, Device>();
        CreateMap<Device, GetDeviceByIdQueryResponse>();
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
