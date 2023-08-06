using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Notiflow.Backoffice.Persistence.Contexts;

public sealed class NotiflowDbContext : DbContext
{
    private readonly Guid _tenantToken;

    public NotiflowDbContext(DbContextOptions<NotiflowDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    //public NotiflowDbContext(DbContextOptions<NotiflowDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    //{
    //    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    //    if (httpContextAccessor?.HttpContext is null)
    //        return;

    //    bool isExists = httpContextAccessor.HttpContext.Request.Headers.TryGetValue("x-tenant-token", out StringValues tenantToken);
    //    if (isExists)
    //    {
    //        _tenantToken = Guid.Parse(tenantToken.Single());
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

       // modelBuilder.Entity<Customer>().HasQueryFilter(customer => customer.TenantToken == _tenantToken);
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<NotificationHistory> NotificationHistories { get; set; }
    public DbSet<EmailHistory> EmailHistories { get; set; }
    public DbSet<TextMessageHistory> TextMessageHistories { get; set; }
}

public class NotiflowDbContextFactory : IDesignTimeDbContextFactory<NotiflowDbContext>
{
    public NotiflowDbContext CreateDbContext(string[] args)
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                           .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                           .AddJsonFile($"appsettings.Localhost.json", optional: true, reloadOnChange: true)
                           .Build();

        var optionsBuilder = new DbContextOptionsBuilder<NotiflowDbContext>();
        optionsBuilder.UseNpgsql(configurationRoot.GetSection(nameof(NotiflowDbContext))["ConnectionString"]);
        optionsBuilder.UseSnakeCaseNamingConvention();

        return new NotiflowDbContext(optionsBuilder.Options);
    }
}