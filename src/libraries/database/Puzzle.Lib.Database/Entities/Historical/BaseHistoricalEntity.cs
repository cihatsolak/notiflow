namespace Puzzle.Lib.Database.Entities.Historical
{
    public abstract class BaseHistoricalEntity : BaseEntity
    {
        /// <summary>
        /// Gets or sets created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets updated date
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
