namespace Puzzle.Lib.Database.Interfaces
{
    /// <summary>
    /// Represents a base interface for a unit of work that manages transactions and saves changes to the database.
    /// </summary>
    /// <remarks>
    /// This interface defines two asynchronous methods for saving changes to the database: SaveChangesAsync and SaveChangesShadowAsync.
    /// </remarks>
    public interface IBaseUnitOfWork
    {
        /// <summary>
        /// Asynchronously saves changes to a shadow database for auditing purposes.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous save operation, which returns the number of objects written to the shadow database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
