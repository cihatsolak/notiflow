namespace Notiflow.Backoffice.Persistence.Repositories.TextMessageHistories;

public sealed class TextMessageHistoryWriteRepository : WriteRepository<TextMessageHistory>, ITextMessageHistoryWriteRepository
{
    public TextMessageHistoryWriteRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }
}
