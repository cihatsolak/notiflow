namespace Notiflow.Backoffice.Persistence.Repositories.EmailHistories;

public sealed class EmailHistoryReadRepository(NotiflowDbContext notiflowDbContext) 
    : ReadRepository<EmailHistory>(notiflowDbContext), IEmailHistoryReadRepository
{
}
