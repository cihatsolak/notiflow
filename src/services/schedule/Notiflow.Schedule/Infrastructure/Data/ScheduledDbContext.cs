namespace Notiflow.Schedule.Infrastructure.Data;

public class ScheduledDbContext : DbContext
{
    public ScheduledDbContext(DbContextOptions<ScheduledDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<ScheduledTextMessage> ScheduledTextMessages { get; set; }
    public DbSet<ScheduledNotification> ScheduledNotifications { get; set; }
    public DbSet<ScheduledEmail> ScheduledEmails { get; set; }
}
