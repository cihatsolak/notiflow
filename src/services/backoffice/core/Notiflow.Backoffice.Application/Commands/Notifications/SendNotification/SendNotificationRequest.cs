namespace Notiflow.Backoffice.Application.Commands.Notifications.SendNotification;

public sealed record SendNotificationRequest : IRequest<ResponseModel<Unit>>
{
    public string Title { get; set; }
}
