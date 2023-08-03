﻿namespace Notiflow.Backoffice.Persistence.Contexts;

public sealed class NotiflowDbContext : DbContext
{
    public NotiflowDbContext(DbContextOptions<NotiflowDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<NotificationHistory> NotificationHistories { get; set; }
    public DbSet<EmailHistory> EmailHistories { get; set; }
    public DbSet<TextMessageHistory> TextMessageHistories { get; set; }
}