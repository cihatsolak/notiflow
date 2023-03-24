namespace Puzzle.Lib.Database.Interfaces.Repositories
{
    /// <summary>
    /// Represents a generic repository for working with entities of type TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the repository works with.</typeparam>
    /// <remarks>
    /// This interface defines methods for working with entities, such as adding, updating, and deleting them.
    /// The 'out' modifier on the TEntity type parameter indicates that this is a covariant interface, meaning that it can be safely used as a read-only interface for TEntity objects.
    /// </remarks>
    public interface IRepository<out TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets the queryable representation of the entity set with change tracking enabled.
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Gets the queryable representation of the entity set without change tracking.
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        /// <summary>
        /// Gets the queryable representation of the entity set without change tracking and with identity resolution.
        /// </summary>
        IQueryable<TEntity> TableNoTrackingWithIdentityResolution { get; }
    }
}
