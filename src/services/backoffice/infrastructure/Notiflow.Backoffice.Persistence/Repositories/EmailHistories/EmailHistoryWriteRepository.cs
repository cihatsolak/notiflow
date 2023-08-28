namespace Notiflow.Backoffice.Persistence.Repositories.EmailHistories;

public sealed class EmailHistoryWriteRepository : WriteRepository<EmailHistory>, IEmailHistoryWriteRepository
{
    public EmailHistoryWriteRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }
}
