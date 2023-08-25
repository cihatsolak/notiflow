namespace Notiflow.Backoffice.Persistence.Repositories.EmailHistories;

public sealed class EmailHistoryReadRepository : ReadRepository<EmailHistory>, IEmailHistoryReadRepository
{
    public EmailHistoryReadRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }
}
