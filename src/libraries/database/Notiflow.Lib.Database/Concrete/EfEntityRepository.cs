using Microsoft.EntityFrameworkCore;

namespace Notiflow.Lib.Database.Concrete
{
    public class EfEntityRepository<TEntity> : IEfEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly DbContext _context;
        protected DbSet<TEntity> _entities;

        public EfEntityRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, bool disableTracking = true)
        {
            return await TableTracking(disableTracking).Where(filter).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params string[] includeProperties)
        {
            IQueryable<TEntity> entityTable = Table;

            if (filter is not null)
                entityTable = entityTable.Where(filter);

            if (orderBy is not null)
                entityTable = orderBy(entityTable);

            if (includeProperties is not null)
                entityTable.Includes(includeProperties);

            return await entityTable.ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool disableTracking = true)
        {
            return await TableTracking(disableTracking).FirstOrDefaultAsync(filter);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params string[] includes)
        {
            return await Table.Includes(includes).FirstOrDefaultAsync(filter);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = TableTracking(disableTracking);

            if (orderBy is not null)
                entityTable = orderBy(entityTable);

            return await entityTable.FirstOrDefaultAsync(filter);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await _entities.FindAsync(id);
        }

        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entities.AddAsync(entity, cancellationToken);
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            ValidateEntities(entities);
            await _entities.AddRangeAsync(entities, cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (_context.Entry(entity).State == EntityState.Detached)
                _entities.Attach(entity);

            _entities.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            _entities.UpdateRange(entities);
        }

        public void DeleteByPropertyName(TEntity entity, string propertyName)
        {
            ArgumentNullException.ThrowIfNull(entity);
            ArgumentException.ThrowIfNullOrEmpty(propertyName);

            if (entity.GetType().GetProperty(propertyName) is null)
                throw new ArgumentException(ExceptionMessage.PropertyValueRequired, nameof(propertyName));

            entity.GetType().GetProperty(propertyName).SetValue(entity, true);
            _entities.Update(entity);
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

            if (_context.Entry(entity).State == EntityState.Detached)
                _entities.Attach(entity);

            _entities.Remove(entity);
        }

        public virtual void DeleteRange()
        {
            _entities.RemoveRange(_entities.AsQueryable());
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            _entities.RemoveRange(entities);
        }

        public virtual void DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            _entities.RemoveRange(_entities.Where(predicate));
        }

        public IQueryable<TEntity> Table => _entities;
        public IQueryable<TEntity> TableNoTracking => _entities.AsNoTracking();
        private IQueryable<TEntity> TableTracking(bool disableTracking) => disableTracking ? TableNoTracking : Table;

        private static void ValidateEntities(IEnumerable<TEntity> entities)
        {
            if (!(entities?.Any() ?? false))
                throw new ArgumentNullException(nameof(entities), ExceptionMessage.EntityRequired);
        }
    }
}
