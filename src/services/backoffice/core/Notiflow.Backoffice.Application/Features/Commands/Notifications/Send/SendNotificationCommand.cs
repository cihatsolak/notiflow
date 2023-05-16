namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;

public sealed record SendNotificationCommand : IRequest<Response<Unit>>
{
    public required string Title { get; init; }
}
