namespace Notiflow.Schedule.Infrastructure.Data;

public class ScheduledDbContextFactory : IDesignTimeDbContextFactory<ScheduledDbContext>
{
    public ScheduledDbContext CreateDbContext(string[] args)
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                           .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                           .AddJsonFile($"appsettings.Localhost.json", optional: true, reloadOnChange: true)
                           .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ScheduledDbContext>();
        optionsBuilder.UseSqlServer(configurationRoot.GetSection(nameof(ScheduledDbContext))[nameof(SqlSetting.ConnectionString)]);

        return new ScheduledDbContext(optionsBuilder.Options);
    }
}