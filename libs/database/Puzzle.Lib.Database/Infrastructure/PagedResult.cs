namespace Puzzle.Lib.Database.Infrastructure;

public sealed record PagedResult<TEntity> where TEntity : class, new()
{
    public required int PageIndex { get; set; }
    public required int PageSize { get; set; }
    public required int TotalPages { get; set; }
    public required int TotalRecords { get; set; }
    public required IEnumerable<TEntity> Items { get; set; }

    public bool IsFirstPage => PageIndex == 1;
    public bool IsLastPage => PageIndex == TotalPages;
}
