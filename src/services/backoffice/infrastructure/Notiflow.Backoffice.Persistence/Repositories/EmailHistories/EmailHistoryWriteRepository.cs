namespace Notiflow.Backoffice.Persistence.Repositories.EmailHistories;

public sealed class EmailHistoryWriteRepository(NotiflowDbContext notiflowDbContext) 
    : WriteRepository<EmailHistory>(notiflowDbContext), IEmailHistoryWriteRepository
{
}
