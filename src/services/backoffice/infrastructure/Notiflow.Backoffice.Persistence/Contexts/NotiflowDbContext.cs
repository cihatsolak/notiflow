namespace Notiflow.Backoffice.Persistence.Contexts
{
    public sealed class NotiflowDbContext : DbContext
    {
        public NotiflowDbContext(DbContextOptions<NotiflowDbContext> options) : base(options)
        {
        }
    }
}