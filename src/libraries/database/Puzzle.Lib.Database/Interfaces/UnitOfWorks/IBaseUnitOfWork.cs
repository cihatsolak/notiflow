namespace Puzzle.Lib.Database.Interfaces.UnitOfWorks
{
    /// <summary>
    /// Represents a base unit of work <see cref="DbContext"/>
    /// </summary>
    public interface IBaseUnitOfWork
    {
        /// <summary>
        /// Tracked changes are added to the database, updated or deleted
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <returns>number of affected entities</returns>
        /// <exception cref="DatabaseOperationException">thrown if database operation is not successful</exception>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Tracked changes are added to the database, updated or deleted. If the entity has inherited from the base historical entity, the corresponding values are added/updated by ef core.
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="cancellationToken">token to cancel asynchronous operation</param>
        /// <returns>number of affected entities</returns>
        /// <exception cref="DatabaseOperationException">thrown if database operation is not successful</exception>
        Task<int> SaveChangesShadowAsync(CancellationToken cancellationToken = default);
    }
}
