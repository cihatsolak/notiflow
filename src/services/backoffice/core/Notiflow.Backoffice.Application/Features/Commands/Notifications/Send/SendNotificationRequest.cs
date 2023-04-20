namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;

public sealed record SendNotificationRequest : IRequest<ResponseModel<Unit>>
{
    public string Title { get; init; }
}
