using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Notiflow.IdentityServer.Core.Entities.Users;

namespace Notiflow.IdentityServer.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    private readonly Guid _tenantToken;

    //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    //{
       
    //}

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        if (httpContextAccessor.HttpContext is not null)
        {
            bool isExists = httpContextAccessor.HttpContext.Request.Headers.TryGetValue("x-tenant-token", out StringValues tenantToken);
            if (isExists)
            {
                _tenantToken = Guid.Parse(tenantToken.First());
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<User>().HasQueryFilter(p => p.Tenant.Token == _tenantToken);
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantApplication> TenantApplications { get; set; }
    public DbSet<TenantPermission> TenantPermissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
}
