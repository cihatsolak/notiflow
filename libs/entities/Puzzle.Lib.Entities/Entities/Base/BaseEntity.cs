namespace Puzzle.Lib.Entities.Entities.Base;

public abstract class BaseEntity<TPrimaryKey> : IEntity where TPrimaryKey : notnull
{
    /// <summary>
    /// Gets or sets the entity identifier
    /// </summary>
    public TPrimaryKey Id { get; set; }
}
