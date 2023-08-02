namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.SendMultiple;

public sealed record SendMultipleNotificationCommand : IRequest<Response<Unit>>
{
    public required List<int> CustomerIds { get; init; }
    public required string Title { get; init; }
    public required string Message { get; init; }
    public required string ImageUrl { get; init; }
}