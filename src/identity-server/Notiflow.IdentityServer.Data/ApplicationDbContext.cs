namespace Notiflow.IdentityServer.Data;

public sealed class ApplicationDbContext : DbContext
{
    private readonly Guid _tenantToken;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        if (httpContextAccessor.HttpContext is null)
            return;

        bool isExists = httpContextAccessor.HttpContext.Request.Headers.TryGetValue("x-tenant-token", out StringValues tenantToken);
        if (isExists)
        {
            _tenantToken = Guid.Parse(tenantToken.Single());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<User>().HasQueryFilter(user => user.Tenant.Token == _tenantToken);
        modelBuilder.Entity<RefreshToken>().HasQueryFilter(refreshToken => refreshToken.User.Tenant.Token == _tenantToken);
        modelBuilder.Entity<TenantPermission>().HasQueryFilter(tenantPermission => tenantPermission.Tenant.Token == _tenantToken);
        modelBuilder.Entity<TenantApplication>().HasQueryFilter(tenantApplication => tenantApplication.Tenant.Token == _tenantToken);
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantApplication> TenantApplications { get; set; }
    public DbSet<TenantPermission> TenantPermissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}
