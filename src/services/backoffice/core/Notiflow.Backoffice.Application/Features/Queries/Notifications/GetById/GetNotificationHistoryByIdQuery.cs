namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed record GetNotificationHistoryByIdQuery : IRequest<Result<GetNotificationHistoryByIdQueryResult>>
{
    public  int Id { get; init; }
    public GetNotificationHistoryByIdQuery(int id)
    {
        Id = id;
    }
}
