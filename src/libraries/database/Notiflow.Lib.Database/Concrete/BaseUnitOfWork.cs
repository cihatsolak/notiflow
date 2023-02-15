namespace Notiflow.Lib.Database.Concrete
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        private readonly DbContext _context;
        private static readonly ILogger _logger = Log.ForContext(typeof(BaseUnitOfWork));

        public BaseUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.Fatal(dbUpdateException, "An error occurred while saving database transactions.");
                string exceptionMessage = await GetFullErrorTextAndRollbackEntityChangesAsync(dbUpdateException, cancellationToken);
                throw new DatabaseOperationException(exceptionMessage, dbUpdateException);
            }
        }

        public virtual async Task<int> SaveChangesShadowAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (var entityEntry in _context.ChangeTracker.Entries())
                {
                    if (entityEntry.Entity is BaseHistoricalEntity baseHistoricalEntity)
                    {
                        switch (entityEntry.State)
                        {
                            case EntityState.Added:
                                _context.Entry(baseHistoricalEntity).Property(p => p.UpdatedDate).IsModified = false;
                                baseHistoricalEntity.CreatedDate = DateTime.Now;
                                break;

                            case EntityState.Modified:
                                _context.Entry(baseHistoricalEntity).Property(p => p.CreatedDate).IsModified = false;
                                baseHistoricalEntity.UpdatedDate = DateTime.Now;
                                break;
                        }
                    }
                }

                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.Fatal(dbUpdateException, "An error occurred while saving database transactions.");
                string exceptionMessage = await GetFullErrorTextAndRollbackEntityChangesAsync(dbUpdateException, cancellationToken);
                throw new DatabaseOperationException(exceptionMessage, dbUpdateException);
            }
        }

        private async Task<string> GetFullErrorTextAndRollbackEntityChangesAsync(DbUpdateException dbUpdateException, CancellationToken cancellationToken)
        {
            var entries = _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.State = EntityState.Unchanged;
            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return dbUpdateException.ToString();
            }
            catch (Exception exception)
            {
                _logger.Fatal(exception, "The entity state could not be changed.");
                return exception.ToString();
            }
        }
    }
}
