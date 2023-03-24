namespace Puzzle.Lib.Database.Concrete.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Table => _entities;
        public IQueryable<TEntity> TableNoTracking => _entities.AsNoTracking();
        public IQueryable<TEntity> TableNoTrackingWithIdentityResolution => _entities.AsNoTrackingWithIdentityResolution();
    }
}
