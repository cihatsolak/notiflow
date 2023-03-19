using Puzzle.Lib.Database.Interfaces.Entities;

namespace Puzzle.Lib.Database.Interfaces.Repositories.OldVersion
{
    /// <summary>
    /// Provides a generic repository pattern implementation for entities that are based on Entity Framework and implements the <see cref="IEntity"/> interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity for which the repository is defined.</typeparam>
    [Obsolete("It is suitable for use in entity framework 6 and lower versions.")]
    public interface IEfEntityRepositoryOld<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Retrieves a paginated list of all the entities from the database, ordered by the given sorting function.
        /// </summary>
        /// <param name="pageIndex">The index of the page to be retrieved.</param>
        /// <param name="pageSize">The size of the page to be retrieved.</param>
        /// <param name="orderBy">The function to sort the results by.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result of the task is a <see cref="PagedResult{TEntity}"/> object.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="pageIndex"/> or <paramref name="pageSize"/> is less than or equal to zero.</exception>
        Task<PagedResult<TEntity>> GetPageAsync(
            int pageIndex,
            int pageSize,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a paginated list of entities that satisfy a given filtering criterion from the database, ordered by the given sorting function.
        /// </summary>
        /// <param name="pageIndex">The index of the page to be retrieved.</param>
        /// <param name="pageSize">The size of the page to be retrieved.</param>
        /// <param name="filter">The filtering criterion for the entities to be retrieved.</param>
        /// <param name="orderBy">The function to sort the results by.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result of the task is a <see cref="PagedResult{TEntity}"/> object.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="pageIndex"/> or <paramref name="pageSize"/> is less than or equal to zero.</exception>
        Task<PagedResult<TEntity>> GetPageAsync(
           int pageIndex,
           int pageSize,
           Expression<Func<TEntity, bool>> filter,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
           CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a paged result of entities based on the provided filter and ordering criteria.
        /// </summary>
        /// <param name="pageIndex">The index of the page to return.</param>
        /// <param name="pageSize">The number of entities per page.</param>
        /// <param name="filter">The filter to apply to the entities.</param>
        /// <param name="orderBy">The ordering criteria for the entities.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the asynchronous operation.</param>
        /// <param name="includes">The navigation properties to include in the query.</param>
        /// <returns>A paged result of entities based on the provided filter and ordering criteria.</returns>
        Task<PagedResult<TEntity>> GetPageAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Retrieves all the entities that match the given filter, with the option to stop tracking changes by default.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <param name="stopTracking">True to stop tracking changes by default, false otherwise.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of entities that match the given filter.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the filter parameter is null.</exception>
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter,
            bool stopTracking = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all the entities that match the given filter and orders them using the given ordering function.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <param name="orderBy">The ordering function to apply.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of entities that match the given filter, ordered using the given function.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the filter or orderBy parameter is null.</exception>
        Task<IEnumerable<TEntity>> GetAllAsync(
           Expression<Func<TEntity, bool>> filter,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
           CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all the entities that match the given filter, orders them using the given ordering function, and includes the specified related entities.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <param name="orderBy">The ordering function to apply.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="includeProperties">The related entities to include.</param>
        /// <returns>The list of entities that match the given filter, ordered using the given function, and including the specified related entities.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the filter or orderBy parameter is null.</exception>
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            CancellationToken cancellationToken = default,
            params string[] includeProperties);

        /// <summary>
        /// Gets the first entity that matches the specified filter. Returns null if no entity matches the filter.
        /// </summary>
        /// <param name="filter">The filter that the entity must match.</param>
        /// <param name="stopTracking">Indicates whether to stop tracking the entity after it is retrieved.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>The entity that matches the specified filter or null if no entity matches the filter.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the filter is null.</exception>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            bool stopTracking = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single entity based on the provided filter and order.
        /// </summary>
        /// <param name="filter">The filter expression to apply.</param>
        /// <param name="orderBy">The order expression to apply.</param>
        /// <param name="stopTracking">Indicates whether to stop tracking the entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching entity or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the filter or orderBy is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when more than one entity is found.</exception>
        Task<TEntity> GetAsync(
          Expression<Func<TEntity, bool>> filter,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
          bool stopTracking = true,
          CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single entity based on the provided filter, order, and included navigation properties.
        /// </summary>
        /// <param name="filter">The filter expression to apply.</param>
        /// <param name="stopTracking">Indicates whether to stop tracking the entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="includes">The navigation properties to include in the query.</param>
        /// <returns>The matching entity or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the filter or includes is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when more than one entity is found.</exception>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            bool stopTracking = true,
            CancellationToken cancellationToken = default,
            params string[] includes);

        /// <summary>
        /// Asynchronously gets the entity by its id.
        /// </summary>
        /// <param name="id">The id of the entity to get.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="ArgumentNullException">Thrown when the id is null or empty.</exception>
        /// <returns>The entity with the specified id.</returns>
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously inserts the entity into the database.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="ArgumentNullException">Thrown when the entity is null.</exception>
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously inserts a collection of entities into the database.
        /// </summary>
        /// <param name="entities">The collection of entities to insert.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="ArgumentNullException">Thrown when the entities collection is null or empty.</exception>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the specified entity in the context.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <exception cref="ArgumentNullException">Thrown when the entity is null.</exception>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the specified entities in the context.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        /// <exception cref="ArgumentNullException">Thrown when the entities are null.</exception>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the entity by the specified property name.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="propertyName">The name of the property to use for the delete operation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the entity or property name is null.</exception>
        void DeleteByPropertyName(TEntity entity, string propertyName);

        /// <summary>
        /// Deletes the entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <exception cref="ArgumentException">Thrown when the ID is less than or equal to zero.</exception>
        void Delete(int id);

        /// <summary>
        /// Deletes the specified entity from the database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <exception cref="ArgumentNullException">Thrown when the input entity is null.</exception>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes all entities in the database of the current type.
        /// </summary>
        void DeleteRange();

        /// <summary>
        /// Deletes the specified entities from the database.
        /// </summary>
        /// <param name="entities">The entities to be deleted.</param>
        /// <exception cref="ArgumentNullException">Thrown when the input entities are null.</exception>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes all entities that match the specified predicate from the database.
        /// </summary>
        /// <param name="predicate">The predicate to filter entities to be deleted.</param>
        /// <exception cref="ArgumentNullException">Thrown when the input predicate is null.</exception>
        void DeleteRange(Expression<Func<TEntity, bool>> predicate);

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