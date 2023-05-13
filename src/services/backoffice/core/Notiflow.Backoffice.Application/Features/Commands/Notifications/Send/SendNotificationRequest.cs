namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;

public sealed record SendNotificationRequest : IRequest<Response<Unit>>
{
    public string Title { get; init; }
}
