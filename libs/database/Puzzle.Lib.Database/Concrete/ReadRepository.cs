namespace Puzzle.Lib.Database.Concrete;

public class ReadRepository<TEntity> : Repository<TEntity>, IReadRepository<TEntity> where TEntity : class, new()
{
    public ReadRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public virtual async Task<PagedResult<TEntity>> GetPageAsync(
       int pageIndex,
       int pageSize,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
       CancellationToken cancellationToken)
    {
        var query = TableNoTracking;

        int totalRecords = await query.CountAsync(cancellationToken);

        if (orderBy is not null)
            query = orderBy(query);

        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        if (pageIndex < 1)
        {
            pageIndex = 1;
        }
        else if (pageIndex > totalPages)
        {
            pageIndex = totalPages;
        }

        var entities = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TEntity>(entities, pageIndex, pageSize, totalRecords, totalPages);
    }

    public virtual async Task<PagedResult<TEntity>> GetPageAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        CancellationToken cancellationToken)
    {
        var query = TableNoTracking;

        if (filter is not null)
            query = query.Where(filter);

        int totalRecords = await query.CountAsync(cancellationToken);

        if (orderBy is not null)
            query = orderBy(query);

        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        if (pageIndex < 1)
        {
            pageIndex = 1;
        }
        else if (pageIndex > totalPages)
        {
            pageIndex = totalPages;
        }

        var entities = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TEntity>(entities, pageIndex, pageSize, totalRecords, totalPages);
    }

    public virtual async Task<PagedResult<TEntity>> GetPageAsync(
    int pageIndex,
    int pageSize,
    Expression<Func<TEntity, bool>> filter,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
    CancellationToken cancellationToken,
    params Expression<Func<TEntity, object>>[] includes)
    {
        var query = TableNoTrackingWithIdentityResolution;

        if (filter is not null)
            query = query.Where(filter);

        if (includes is not null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        int totalRecords = await query.CountAsync(cancellationToken);

        if (orderBy is not null)
            query = orderBy(query);

        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        if (pageIndex < 1)
        {
            pageIndex = 1;
        }
        else if (pageIndex > totalPages)
        {
            pageIndex = totalPages;
        }

        var entities = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TEntity>(entities, pageIndex, pageSize, totalRecords, totalPages);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        bool stopTracking = true,
        CancellationToken cancellationToken = default)
    {
        return await SelectEntities(stopTracking).Where(filter).ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = Table;

        if (filter is not null)
            query = query.Where(filter);

        if (orderBy is not null)
            query = orderBy(query);

        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        CancellationToken cancellationToken,
        params string[] includeProperties)
    {
        IQueryable<TEntity> query = TableNoTrackingWithIdentityResolution;

        if (filter is not null)
            query = query.Where(filter);

        if (orderBy is not null)
            query = orderBy(query);

        if (includeProperties is not null)
            query.Includes(includeProperties);

        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        bool stopTracking = true,
        CancellationToken cancellationToken = default)
    {
        return await SelectEntities(stopTracking).FirstOrDefaultAsync(filter, cancellationToken);
    }

    public virtual async Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        bool stopTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = SelectEntities(stopTracking);

        if (orderBy is not null)
            query = orderBy(query);

        return await query.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public virtual async Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        bool stopTracking = true,
        CancellationToken cancellationToken = default,
        params string[] includes)
    {
        return await SelectRelationEntities(stopTracking).Includes(includes).FirstOrDefaultAsync(filter, cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken)
    {
        return await _entities.FindAsync(new object[] { id }, cancellationToken);
    }

    private IQueryable<TEntity> SelectEntities(bool stopTracking) => stopTracking ? TableNoTracking : Table;
    private IQueryable<TEntity> SelectRelationEntities(bool stopTracking) => stopTracking ? TableNoTrackingWithIdentityResolution : Table;
}
