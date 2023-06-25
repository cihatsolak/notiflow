namespace Puzzle.Lib.Database.Concrete;

public class BaseUnitOfWork : IBaseUnitOfWork
{
    protected readonly DbContext _context;

    public BaseUnitOfWork(DbContext context)
    {
        _context = context;
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var baseHistoricalEntities = _context.ChangeTracker.Entries<BaseHistoricalEntity>();

        foreach (var baseHistoricalEntity in baseHistoricalEntities)
        {
            switch (baseHistoricalEntity.State)
            {
                case EntityState.Added:
                    baseHistoricalEntity.Property(p => p.UpdatedDate).IsModified = false;
                    baseHistoricalEntity.Entity.CreatedDate = DateTime.Now;
                    break;

                case EntityState.Modified:
                    baseHistoricalEntity.Property(p => p.CreatedDate).IsModified = false;
                    baseHistoricalEntity.Entity.UpdatedDate = DateTime.Now;
                    break;
            }
        }

        var baseSoftDeleteEntities = _context.ChangeTracker.Entries<BaseSoftDeleteEntity>();

        foreach (var baseSoftDeleteEntity in baseSoftDeleteEntities)
        {
            baseSoftDeleteEntity.State = EntityState.Modified;
            baseSoftDeleteEntity.Property(p => p.IsDeleted).CurrentValue = true;
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
