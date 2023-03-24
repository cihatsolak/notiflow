namespace Puzzle.Lib.Database.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// 
        /// </summary>
        DbSet<TEntity> DbSet { get; }

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
