namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.SendNotification;

public sealed record SendNotificationRequest : IRequest<ResponseModel<Unit>>
{
    public string Title { get; set; }
}
