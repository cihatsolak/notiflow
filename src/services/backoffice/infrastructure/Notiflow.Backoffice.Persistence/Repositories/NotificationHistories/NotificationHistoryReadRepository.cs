namespace Notiflow.Backoffice.Persistence.Repositories.NotificationHistories;

public sealed class NotificationHistoryReadRepository(NotiflowDbContext notiflowDbContext) 
    : ReadRepository<NotificationHistory>(notiflowDbContext), INotificationHistoryReadRepository
{
}
