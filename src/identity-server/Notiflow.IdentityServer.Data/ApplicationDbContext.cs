namespace Notiflow.IdentityServer.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantApplication> TenantApplications { get; set; }
    public DbSet<TenantPermission> TenantPermissions { get; set; }
    public DbSet<User> Users { get; set; }
}
