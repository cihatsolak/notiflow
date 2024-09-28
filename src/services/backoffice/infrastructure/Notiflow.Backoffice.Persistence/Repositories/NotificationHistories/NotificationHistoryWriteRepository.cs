namespace Notiflow.Backoffice.Persistence.Repositories.NotificationHistories;

public sealed class NotificationHistoryWriteRepository(NotiflowDbContext notiflowDbContext)
    : WriteRepository<NotificationHistory>(notiflowDbContext), INotificationHistoryWriteRepository
{
}