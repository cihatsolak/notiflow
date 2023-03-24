namespace Puzzle.Lib.Database.Concrete.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public DbSet<TEntity> DbSet => _entities;

        public IQueryable<TEntity> Table => throw new NotImplementedException();

        public IQueryable<TEntity> TableNoTracking => throw new NotImplementedException();

        public IQueryable<TEntity> TableNoTrackingWithIdentityResolution => throw new NotImplementedException();
    }
}
