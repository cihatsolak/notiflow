using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Notiflow.IdentityServer.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantApplication> TenantApplications { get; set; }
    public DbSet<TenantPermission> TenantPermissions { get; set; }
    public DbSet<User> Users { get; set; }
}
