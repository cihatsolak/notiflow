namespace Notiflow.Lib.Database.Abstract
{
    /// <summary>
    /// Represents an base unit of work
    /// </summary>
    public interface IBaseUnitOfWork
    {
        /// <summary>
        /// Tracked changes are added to the database, updated or deleted
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>number of affected records</returns>
        /// <exception cref="Exception">when transferring data to database</exception>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Tracked changes are added to the database, updated or deleted
        /// </summary>
        /// <remarks>method that can be overridden</remarks>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>number of affected records</returns>
        /// <exception cref="Exception">when transferring data to database</exception>
        Task<int> SaveChangesShadowAsync(CancellationToken cancellationToken = default);
    }
}
