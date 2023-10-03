namespace Puzzle.Lib.Entities.Entities.Historical;

public interface IBaseHistoricalEntity : IEntity
{
    DateTime CreatedDate { get; set; }
    DateTime UpdatedDate { get; set; }
}

public abstract class BaseHistoricalEntity<TPrimaryKey> : BaseEntity<TPrimaryKey>, IBaseHistoricalEntity where TPrimaryKey : notnull
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
