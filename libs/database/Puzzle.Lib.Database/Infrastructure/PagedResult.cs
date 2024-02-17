namespace Puzzle.Lib.Database.Infrastructure;

public sealed record PagedResult<TEntity> where TEntity : class, new()
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public IEnumerable<TEntity> Items { get; set; }

    public bool IsFirstPage => PageIndex == 1;
    public bool IsLastPage => PageIndex == TotalPages;


    public PagedResult(IEnumerable<TEntity> entities, int pageIndex, int pageSize, int totalRecords, int totalPages)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = totalPages;
        Items = entities;
    }
}
