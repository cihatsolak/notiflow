namespace Puzzle.Lib.Database.Concrete;

public class WriteRepository<TEntity>(DbContext dbContext) : Repository<TEntity>(dbContext), IWriteRepository<TEntity> where TEntity : class, new()
{
    public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        CheckArguments(entity);

        await _entities.AddAsync(entity, cancellationToken);
    }

    public virtual Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        CheckArguments(entities);

        return _entities.AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        CheckArguments(entity);

        _entities.Update(entity);
    }

    public virtual void Update(IEnumerable<TEntity> entities)
    {
        CheckArguments(entities);

        _entities.UpdateRange(entities);
    }

    public virtual Task<int> ExecuteUpdateAsync(
       Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls,
       CancellationToken cancellationToken)
    {
        return _entities.ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
    }

    public virtual Task<int> ExecuteUpdateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls,
        CancellationToken cancellationToken)
    {
        return _entities.Where(predicate).ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
    }

    public virtual void Delete(TEntity entity)
    {
        CheckArguments(entity);

        _entities.Remove(entity);
    }

    public virtual void Delete(IEnumerable<TEntity> entities)
    {
        CheckArguments(entities);

        _entities.RemoveRange(entities);
    }

    public virtual async Task<bool> ExecuteDeleteAsync(CancellationToken cancellationToken)
    {
        int numberOfRowsDeleted = await _entities.ExecuteDeleteAsync(cancellationToken);
        return numberOfRowsDeleted > 0;
    }

    public virtual async Task<bool> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        int numberOfRowsDeleted = await _entities.Where(predicate).ExecuteDeleteAsync(cancellationToken);
        return numberOfRowsDeleted > 0;
    }

    public virtual async Task<bool> ExecuteDeleteByIdAsync<TProperty>(TProperty id, CancellationToken cancellationToken) where TProperty : struct
    {
        int numberOfRowsDeleted = await _entities.Where(p => EF.Property<TProperty>(p, "Id").Equals(id)).ExecuteDeleteAsync(cancellationToken);
        return numberOfRowsDeleted > 0;
    }

    private static void CheckArguments(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
    }

    private static void CheckArguments(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
    }
}