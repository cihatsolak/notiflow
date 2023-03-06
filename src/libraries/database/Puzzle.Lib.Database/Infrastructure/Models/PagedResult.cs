namespace Puzzle.Lib.Database.Infrastructure.Models
{
    public class PagedResult<TEntity> where TEntity : class, IEntity, new()
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<TEntity> Items { get; set; }
    }
}
