namespace Notiflow.Lib.Database.Abstract
{
    /// <summary>
    /// Represents an entity repository <see cref="DbContext"/>
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public interface IEfEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Get entities by paging by filtering options
        /// </summary>
        /// <remarks>In this method, entities are not tracked by the change tracker. method that can be overridden</remarks>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <param name="filter">filtering options</param>
        /// <param name="orderBy">option to sort entities</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <returns>According to the filtering options, if there is an entity, it will return, otherwise a blank page will be returned.</returns>
        Task<PagedResult<TEntity>> GetPageAsync(
            int pageIndex,
            int pageSize,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get entities by paging by filtering options
        /// </summary>
        /// <remarks>In this method, entities are not tracked by the change tracker. method that can be overridden</remarks>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <param name="filter">filtering options</param>
        /// <param name="orderBy">option to sort entities</param>
        /// <param name="includes">table information to be related</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <returns>According to the filtering options, if there is an entity, it will return, otherwise a blank page will be returned.</returns>
        Task<PagedResult<TEntity>> GetPageAsync(
            int pageIndex,
            int pageSize,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get entities by filtering
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="filter">expresssion filter</param>
        /// <param name="disableTracking">ef change tracker active/passive</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <returns>If the filtering options have entities, it returns the related entities. otherwise it returns empty entity list</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter,
            bool disableTracking = true, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get entities by filtering and sorting options
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="filter">filtering options on entities</param>
        /// <param name="orderBy">sort option on requested entity</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <param name="includeProperties">include string filter</param>
        /// <returns>If the filtering options have entities, it returns the related entities. otherwise it returns empty entity list</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            CancellationToken cancellationToken = default,
            params string[] includeProperties);

        /// <summary>
        /// Get entity by filtering
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="filter">filtering options on entities</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <param name="disableTracking">ef change tracker active/passive</param>
        /// <returns>If the entity of the filtering options exists, it returns the related entity. otherwise it returns null</returns>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            bool disableTracking = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get entity by filtering
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="filter">filtering options on entities</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <param name="includes">relation ship entity names</param>
        /// <returns>If the entity of the filtering options exists, it returns the related entity. otherwise it returns null</returns>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default,
            params string[] includes);

        /// <summary>
        /// Get entity by filtering and sorting options
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="filter">filtering options on entities</param>
        /// <param name="orderBy">sort option on requested entity</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <param name="disableTracking">ef change tracker active/passive</param>
        /// <returns>If the entity of the filtering options exists, it returns the related entity. otherwise it returns null</returns>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,            
            bool disableTracking = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get entity by primary key / entity identifier
        /// </summary>
        /// <remarks>should only be searched by primary key value. method that can be overridden</remarks>
        /// <param name="id">entity primary-key/identifier</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <returns>If the identifier has an entity, it returns the related entity. otherwise it returns null</returns>
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds the entity to the database
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="entity">entity to be added to the database</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <exception cref="ArgumentNullException">thrown when entity is null or empty<exception>
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds entities to database
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="entities">entity list to be added to the database</param>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <exception cref="ArgumentNullException">thrown when entity list is null or empty<exception>
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
        /// Entity table tracked by change tracker
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }
    }
}