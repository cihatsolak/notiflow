namespace Notiflow.Backoffice.Persistence.Repositories.NotificationHistories;

public sealed class NotificationHistoryWriteRepository : WriteRepository<NotificationHistory>, INotificationHistoryWriteRepository
{
    public NotificationHistoryWriteRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }
}