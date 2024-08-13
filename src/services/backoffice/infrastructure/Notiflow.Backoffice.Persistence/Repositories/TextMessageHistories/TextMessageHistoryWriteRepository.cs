namespace Notiflow.Backoffice.Persistence.Repositories.TextMessageHistories;

public sealed class TextMessageHistoryWriteRepository(NotiflowDbContext notiflowDbContext) : WriteRepository<TextMessageHistory>(notiflowDbContext), ITextMessageHistoryWriteRepository
{
}
