namespace Notiflow.Backoffice.Persistence.Repositories.NotificationHistories;

public sealed class NotificationHistoryReadRepository : ReadRepository<NotificationHistory>, INotificationHistoryReadRepository
{
    public NotificationHistoryReadRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }
}
