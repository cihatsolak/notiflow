using Notiflow.Backoffice.Application.Features.Commands.Devices;
using Notiflow.Backoffice.Application.Features.Commands.Emails;
using Notiflow.Backoffice.Application.Features.Commands.Notifications;
using Notiflow.Backoffice.Application.Features.Commands.TextMessages;
using Notiflow.Backoffice.Application.Features.Queries.Customers;
using Notiflow.Backoffice.Application.Features.Queries.Devices;
using Notiflow.Backoffice.Application.Features.Queries.Emails;
using Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories;

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

        CreateMap<Customer, CustomerResponse>();
    }

    private void DeviceMaps()
    {
        CreateMap<AddDeviceCommand, Device>();
        CreateMap<Device, DeviceResponse>();

        CreateMap<Device, DeviceDataTableResult>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Customer.Name} {src.Customer.Surname}"));
    }

    private void TextMessageHistoryMaps()
    {
        CreateMap<TextMessageHistory, TextMessageHistoryResponse>();
        CreateMap<TextMessageHistory, TextMessageDataTableCommandResult>();

        CreateMap<SendTextMessageCommand, TextMessageDeliveredEvent>();
        CreateMap<SendTextMessageCommand, TextMessageNotDeliveredEvent>();
    }

    private void NotificationMaps()
    {
        CreateMap<NotificationHistory, NotificationHistoryResponse>();
        CreateMap<SendNotificationCommand, NotificationNotDeliveredEvent>();
        CreateMap<SendNotificationCommand, NotificationDeliveredEvent>();
    }

    private void EmailMaps()
    {
        CreateMap<SendEmailCommand, EmailDeliveredEvent>();
        CreateMap<SendEmailCommand, EmailNotDeliveredEvent>();
        CreateMap<SendEmailCommand, EmailRequest>();

        CreateMap<EmailHistory, EmailHistoryResponse>();
    }
}
