namespace Notiflow.Backoffice.Application.Interfaces.Repositories;

public interface ITextMessageHistoryReadRepository : IReadRepository<TextMessageHistory>
{
    Task<(int recordsTotal, List<TextMessageHistory> textMessageHistories)> GetPageAsync(string sortKey, string searchKey, int pageIndex, int pageSize, CancellationToken cancellationToken);
}

public interface ITextMessageHistoryWriteRepository : IWriteRepository<TextMessageHistory>
{
}