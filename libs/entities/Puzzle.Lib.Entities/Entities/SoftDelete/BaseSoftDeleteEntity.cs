namespace Puzzle.Lib.Entities.Entities.SoftDelete;

public interface IBaseSoftDeleteEntity : IEntity
{
    bool IsDeleted { get; set; }
}

public abstract class BaseSoftDeleteEntity<TPrimaryKey> : BaseEntity<TPrimaryKey>, IBaseSoftDeleteEntity where TPrimaryKey : notnull
{
    /// <summary>
    /// Gets or sets is deleted
    /// </summary>
    public bool IsDeleted { get; set; }
}
