namespace Notiflow.IdentityServer.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
   

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<User>().HasQueryFilter(p => p.Tenant.Token == Guid.Parse("43A7E7C5-DA5C-4C96-BF1E-D547C0B70B82"));

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantApplication> TenantApplications { get; set; }
    public DbSet<TenantPermission> TenantPermissions { get; set; }
    public DbSet<User> Users { get; set; }
}
