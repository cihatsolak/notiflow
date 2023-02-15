namespace Notiflow.Lib.Database.Abstract
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public interface IEfEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Get entities by expression fiter
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="filter">expresssion filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>entities</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, bool disableTracking = true);

        /// <summary>
        /// Get entities by expression fiter with ordering
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="filter">expresssion filter</param>
        /// <param name="orderBy">orderby filter</param>
        /// <param name="includeProperties">include string filter</param>
        /// <returns>entities</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params string[] includeProperties);

        /// <summary>
        /// Get entity by expression fiter
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="filter">expresssion filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>entity</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool disableTracking = true);

        /// <summary>
        /// Get entity by expression fiter and include properties
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="filter">expresssion filter</param>
        /// <param name="includes">relation ship entity names</param>
        /// <returns>entity</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params string[] includes);

        /// <summary>
        /// Get entity by expression fiter
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="filter">expresssion filter</param>
        /// <param name="orderBy">orderby filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>entity</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool disableTracking = true);

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="id">identifier</param>
        /// <returns>entity</returns>
        Task<TEntity> GetByIdAsync(object id);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="entity">entity</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <exception cref="ArgumentNullException">when method parameter is null</exception>
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="entities">entities</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <exception cref="ArgumentNullException">when method parameter is null or not any</exception>
        Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="entity">entity</param>
        /// <exception cref="ArgumentNullException">when method parameter is null</exception>
        void Update(TEntity entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="entity">entity</param>
        /// <exception cref="ArgumentNullException">when method parameter is null or not any</exception>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete by property name
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="entity">entity</param>
        /// <param name="propertyName">property name</param>
        /// <exception cref="ArgumentException">when there are no entity for the property</exception>
        void DeleteByPropertyName(TEntity entity, string propertyName);

        /// <summary>
        ///  Delete entity
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="id">primary key</param>
        /// <exception cref="ArgumentNullException">when entity with id value is not found</exception>
        void Delete(int id);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="entity">entity</param>
        /// <exception cref="ArgumentNullException">when method parameter is null</exception>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete entities
        /// <remarks>warning: deletes all rows in the table. method that can be overridden</remarks>
        /// </summary>
        /// <exception cref="ArgumentNullException">when method parameter is null or not any</exception>
        void DeleteRange();

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="entities">entities</param>
        /// <exception cref="ArgumentNullException">when method parameter is null or not any</exception>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="predicate">expresssion filter</param>
        /// <exception cref="ArgumentNullException">when method parameter is null or not any</exception>
        void DeleteRange(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }
    }
}