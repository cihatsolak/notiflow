namespace Puzzle.Lib.Database.Interfaces.Repositories
{
    public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
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
    }
}
