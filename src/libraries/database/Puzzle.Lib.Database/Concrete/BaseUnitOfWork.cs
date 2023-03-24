﻿namespace Puzzle.Lib.Database.Concrete
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        protected readonly DbContext _context;
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
                await GetFullErrorTextAndRollbackEntityChangesAsync(cancellationToken);
                throw;
            }
        }

        public virtual async Task<int> SaveChangesShadowAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var baseHistoricalEntryEntities = _context.ChangeTracker.Entries<BaseHistoricalEntity>();

                foreach (var baseHistoricalEntity in baseHistoricalEntryEntities)
                {
                    switch (baseHistoricalEntity.State)
                    {
                        case EntityState.Added:
                            _context.Entry(baseHistoricalEntity.Entity).Property(p => p.UpdatedDate).IsModified = false;
                            baseHistoricalEntity.Entity.CreatedDate = DateTime.Now;
                            break;

                        case EntityState.Modified:
                            _context.Entry(baseHistoricalEntity.Entity).Property(p => p.CreatedDate).IsModified = false;
                            baseHistoricalEntity.Entity.UpdatedDate = DateTime.Now;
                            break;
                    }
                }

                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.Fatal(dbUpdateException, "An error occurred while saving database transactions.");
                await GetFullErrorTextAndRollbackEntityChangesAsync(cancellationToken);
                throw;
            }
        }

        private async Task GetFullErrorTextAndRollbackEntityChangesAsync(CancellationToken cancellationToken)
        {
            var entries = _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.State = EntityState.Unchanged;
            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.Fatal(exception, "The entity state could not be changed.");
            }
        }
    }
}
