namespace Notiflow.IdentityServer.Data;

public sealed class ApplicationDbContext : DbContext
{
    private const string TENANT_TOKEN_HEADER = "x-tenant-token";
    private readonly Guid _tenantToken;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options, 
        IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _tenantToken = GetTenantToken(httpContextAccessor);
    }

    private static Guid GetTenantToken(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext is null)
            return Guid.Empty;

        bool isExists = httpContextAccessor.HttpContext.Request.Headers.TryGetValue(TENANT_TOKEN_HEADER, out StringValues tenantToken);
        if (!isExists)
            return Guid.Empty;

        return Guid.Parse(tenantToken.Single());
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
    public DbSet<UserOutbox> UserOutboxes { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}
