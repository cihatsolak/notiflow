namespace Puzzle.Lib.Entities.Entities.HistoricalSoftDelete
{
    public abstract class BaseHistoricalSoftDeleteEntity<TPrimaryKey> : BaseHistoricalEntity<TPrimaryKey> where TPrimaryKey : notnull
    {
        /// <summary>
        /// Gets or sets is deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
