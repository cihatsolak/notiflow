namespace Puzzle.Lib.Database.Concrete;

public class BaseUnitOfWork : IBaseUnitOfWork
{
    protected readonly DbContext _context;

    public BaseUnitOfWork(DbContext context)
    {
        _context = context;
    }

    public virtual async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) 
        => await _context.Database.BeginTransactionAsync(cancellationToken);

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
