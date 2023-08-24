namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed record GetNotificationHistoryByIdQuery : IRequest<Response<GetNotificationHistoryByIdQueryResponse>>
{
    public required int Id { get; init; }
}
