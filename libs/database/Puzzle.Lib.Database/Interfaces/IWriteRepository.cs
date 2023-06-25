namespace Puzzle.Lib.Database.Interfaces;

/// <summary>
/// Represents a repository for writing entities of type TEntity.
/// </summary>
/// <typeparam name="TEntity">The type of entity to write.</typeparam>
/// <remarks>
/// This interface extends the IRepository<TEntity> interface and adds methods for creating, updating, and deleting entities.
/// The TEntity type parameter must be a class that derives from the BaseEntity class.
/// </remarks>
public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
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
    /// Executes an update query asynchronously for the given entity using the specified property calls.
    /// </summary>
    /// <param name="setPropertyCalls">The property calls used to update the entity.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation that returns the number of entities updated.</returns>
    Task<int> ExecuteUpdateAsync(
       Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls,
       CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes an update query asynchronously for entities that match the specified predicate using the specified property calls.
    /// </summary>
    /// <param name="predicate">The predicate used to filter entities for the update.</param>
    /// <param name="setPropertyCalls">The property calls used to update the entities.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation that returns the number of entities updated.</returns>
    Task<int> ExecuteUpdateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified entity from the database.
    /// </summary>
    /// <param name="entity">The entity to be deleted.</param>
    /// <exception cref="ArgumentNullException">Thrown when the input entity is null.</exception>
    void Delete(TEntity entity);

    /// <summary>
    /// Deletes the specified entities from the database.
    /// </summary>
    /// <param name="entities">The entities to be deleted.</param>
    /// <exception cref="ArgumentNullException">Thrown when the input entities are null.</exception>
    void Delete(IEnumerable<TEntity> entities);

    /// <summary>
    /// Deletes all entities in the database of the current type.
    /// <param name="cancellationToken">The cancellation token.</param>
    /// </summary>
    Task<int> ExecuteDeleteAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the entity with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="ArgumentException">Thrown when the ID is less than or equal to zero.</exception>
    Task<int> ExecuteDeleteAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes all entities that match the specified predicate from the database.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities to be deleted.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="ArgumentNullException">Thrown when the input predicate is null.</exception>
    Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}