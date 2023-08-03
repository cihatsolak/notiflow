namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.SendSingle;

public sealed record SendSingleNotificationCommand : IRequest<Response<Unit>>
{
    public required int CustomerId { get; init; }
    public required string Title { get; init; }
    public required string Message { get; init; }
    public required string ImageUrl { get; init; }
}
