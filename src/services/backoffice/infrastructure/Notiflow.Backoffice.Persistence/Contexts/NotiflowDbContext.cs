namespace Notiflow.Backoffice.Persistence.Contexts;

public sealed class NotiflowDbContext : DbContext
{
    private readonly int _tenantId;

    public NotiflowDbContext(DbContextOptions<NotiflowDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        if (httpContextAccessor?.HttpContext is null)
            return;

        string tenantId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.PrimaryGroupSid)?.Value;
        if (!string.IsNullOrWhiteSpace(tenantId))
        {
            _tenantId = int.Parse(tenantId);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Customer>()
             .HasQueryFilter(customer => !customer.IsBlocked &&
                                         !customer.IsDeleted &&
                                          customer.TenantId == _tenantId);
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<NotificationHistory> NotificationHistories { get; set; }
    public DbSet<EmailHistory> EmailHistories { get; set; }
    public DbSet<TextMessageHistory> TextMessageHistories { get; set; }
}
