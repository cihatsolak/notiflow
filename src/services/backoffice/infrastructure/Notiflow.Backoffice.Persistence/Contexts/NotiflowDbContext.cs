using Notiflow.Backoffice.Application.Exceptions;

namespace Notiflow.Backoffice.Persistence.Contexts;

public sealed class NotiflowDbContext : DbContext
{
    private readonly int _tenantId;

    public NotiflowDbContext(DbContextOptions<NotiflowDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        if (httpContextAccessor?.HttpContext is null)
            return;

        var tenantIdClaim = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.PrimaryGroupSid);
        if (tenantIdClaim is null)
        {
            throw new TenantException("Unauthorized transaction detected.");
        }
        else
        {
            _ = int.TryParse(tenantIdClaim.Value, out _tenantId);
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
