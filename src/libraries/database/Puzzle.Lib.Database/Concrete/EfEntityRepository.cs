namespace Puzzle.Lib.Database.Concrete
{
    public class EfEntityRepository<TEntity> : IEfEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public EfEntityRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task<PagedResult<TEntity>> GetPageAsync(
            int pageIndex,
            int pageSize,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, bool>> filter = null,
            CancellationToken cancellationToken = default)
        {
            var entityTable = TableNoTracking;

            if (filter is not null)
                entityTable = entityTable.Where(filter);

            int totalRecords = await entityTable.CountAsync(cancellationToken);

            if (orderBy is not null)
                entityTable = orderBy(entityTable);

            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            else if (pageIndex > totalPages)
            {
                pageIndex = totalPages;
            }

            var entities = await entityTable
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<TEntity>
            {
                Items = entities,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        }

        public async Task<PagedResult<TEntity>> GetPageAsync(
        int pageIndex,
        int pageSize,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        Expression<Func<TEntity, bool>> filter = null,
        CancellationToken cancellationToken = default,        
        params Expression<Func<TEntity, object>>[] includes)
        {
            var entityTable = TableNoTracking;

            if (filter is not null)
                entityTable = entityTable.Where(filter);

            if (includes is not null)
                entityTable = includes.Aggregate(entityTable, (current, include) => current.Include(include));

            int totalRecords = await entityTable.CountAsync(cancellationToken);

            if (orderBy is not null)
                entityTable = orderBy(entityTable);

            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            else if (pageIndex > totalPages)
            {
                pageIndex = totalPages;
            }

            var entities = await entityTable
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<TEntity>
            {
                Items = entities,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter,
            bool disableTracking = true,
            CancellationToken cancellationToken = default)
        {
            return await TableTracking(disableTracking).Where(filter).ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            CancellationToken cancellationToken = default,
            params string[] includeProperties)
        {
            IQueryable<TEntity> entityTable = Table;

            if (filter is not null)
                entityTable = entityTable.Where(filter);

            if (orderBy is not null)
                entityTable = orderBy(entityTable);

            if (includeProperties is not null)
                entityTable.Includes(includeProperties);

            return await entityTable.ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter, 
            bool disableTracking = true,
            CancellationToken cancellationToken = default)
        {
            return await TableTracking(disableTracking).FirstOrDefaultAsync(filter, cancellationToken);
        }

        public virtual async Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default,
            params string[] includes)
        {
            return await Table.Includes(includes).FirstOrDefaultAsync(filter, cancellationToken);
        }

        public virtual async Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, 
            bool disableTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> entityTable = TableTracking(disableTracking);

            if (orderBy is not null)
                entityTable = orderBy(entityTable);

            return await entityTable.FirstOrDefaultAsync(filter, cancellationToken);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await _entities.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ValidateEntity(entity);
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
                throw new ArgumentNullException(nameof(propertyName));

            entity.GetType().GetProperty(propertyName).SetValue(entity, true);

            if (_context.Entry(entity).State == EntityState.Detached)
                _entities.Attach(entity);

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

        private static void ValidateEntity(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
        }

        private static void ValidateEntities(IEnumerable<TEntity> entities)
        {
            if (!(entities?.Any() ?? false))
                throw new ArgumentNullException(nameof(entities));
        }
    }
}
