namespace Puzzle.Lib.Database.Abstract
{
    public abstract class BaseEntity : IEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }
    }

    public abstract class BaseSoftDeleteEntity : BaseEntity
    {
        /// <summary>
        /// Gets or sets is deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }

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

    public abstract class BaseHistoricalSoftDeleteEntity : BaseHistoricalEntity
    {
        /// <summary>
        /// Gets or sets is deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
