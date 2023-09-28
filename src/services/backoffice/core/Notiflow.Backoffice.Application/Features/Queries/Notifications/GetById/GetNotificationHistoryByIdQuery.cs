namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed record GetNotificationHistoryByIdQuery : IRequest<ApiResponse<GetNotificationHistoryByIdQueryResult>>
{
    public required int Id { get; init; }
}
