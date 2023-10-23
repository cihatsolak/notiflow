namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed record GetNotificationHistoryByIdQuery : IRequest<Result<GetNotificationHistoryByIdQueryResult>>
{
    public required int Id { get; init; }
}
