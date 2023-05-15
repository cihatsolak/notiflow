namespace Notiflow.Backoffice.Persistence.Repositories.TextMessageHistories;

public sealed class TextMessageHistoryReadRepository : ReadRepository<TextMessageHistory>, ITextMessageHistoryReadRepository
{
    public TextMessageHistoryReadRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }
}
