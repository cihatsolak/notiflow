namespace Notiflow.Backoffice.Persistence.Repositories.TextMessageHistories;

public sealed class TextMessageHistoryReadRepository(NotiflowDbContext notiflowDbContext) 
    : ReadRepository<TextMessageHistory>(notiflowDbContext), ITextMessageHistoryReadRepository
{
    public async Task<(int recordsTotal, List<TextMessageHistory> textMessageHistories)> GetPageAsync(string sortKey, string searchKey, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var textMessageHistoryTable = TableNoTrackingWithIdentityResolution.IgnoreQueryFilters();

        if (string.IsNullOrWhiteSpace(sortKey))
            textMessageHistoryTable = textMessageHistoryTable.OrderBy(sortKey);
        else
            textMessageHistoryTable = textMessageHistoryTable.OrderByDescending(customer => customer.SentDate);

        if (string.IsNullOrWhiteSpace(searchKey))
        {
            textMessageHistoryTable = textMessageHistoryTable.Where(textMessageHistory => string.Concat(textMessageHistory.Message, textMessageHistory.ErrorMessage).IndexOf(searchKey) > -1);
        }

        int recordsTotal = await textMessageHistoryTable.CountAsync(cancellationToken);

        var customers = await textMessageHistoryTable
            .TagWith("Lists text message history records by paging.")
            .Skip(pageIndex)
            .Take(pageSize)
            .Select(textMessageHistory => new TextMessageHistory
            {
                Id = textMessageHistory.Id,
                Message = textMessageHistory.Message,
                IsSent = textMessageHistory.IsSent,
                SentDate = textMessageHistory.SentDate
                
            })
            .ToListAsync(cancellationToken);

        return (recordsTotal, customers);
    }
}
