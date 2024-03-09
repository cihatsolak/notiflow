namespace Puzzle.Lib.Database.Concrete;

[Obsolete("It is suitable for use in entity framework 6 and lower versions.")]
public class Ef6EntityRepository<TEntity> : IEf6EntityRepository<TEntity> where TEntity : class, new()
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _entities;

    public Ef6EntityRepository(DbContext context)
    {
        _context = context;
        _entities = context.Set<TEntity>();
    }

    public virtual async Task<PagedResult<TEntity>> GetPageAsync(
        int pageIndex,
        int pageSize,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        CancellationToken cancellationToken = default)
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
        CancellationToken cancellationToken = default)
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
    CancellationToken cancellationToken = default,
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
        CancellationToken cancellationToken = default)
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
        CancellationToken cancellationToken = default,
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

    public virtual async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        return await _entities.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await _entities.AddAsync(entity, cancellationToken);
    }

    public virtual async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entities);

        await _entities.AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _entities.Update(entity);
    }

    public virtual void Update(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        _entities.UpdateRange(entities);
    }

    public virtual void Delete()
    {
        _entities.RemoveRange(_entities.AsQueryable());
    }

    public virtual void Delete(int id)
    {
        var entity = _entities.Find(id);
        if (entity is not null)
        {
            Delete(entity);
        }
    }

    public virtual void Delete(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _entities.Remove(entity);
    }

    public virtual void Delete(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        _entities.RemoveRange(entities);
    }

    public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
    {
        _entities.RemoveRange(_entities.Where(predicate));
    }

    public virtual void DeleteByPropertyName(TEntity entity, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentException.ThrowIfNullOrEmpty(propertyName);

        if (entity.GetType().GetProperty(propertyName) is null)
            throw new ArgumentNullException(nameof(propertyName));

        entity.GetType().GetProperty(propertyName).SetValue(entity, true);

        _entities.Update(entity);
    }

    public IQueryable<TEntity> Table => _entities;
    public IQueryable<TEntity> TableNoTracking => _entities.AsNoTracking();
    public IQueryable<TEntity> TableNoTrackingWithIdentityResolution => _entities.AsNoTrackingWithIdentityResolution();

    private IQueryable<TEntity> SelectEntities(bool stopTracking) => stopTracking ? TableNoTracking : Table;
    private IQueryable<TEntity> SelectRelationEntities(bool stopTracking) => stopTracking ? TableNoTrackingWithIdentityResolution : Table;
}
