namespace Notiflow.Schedule.Infrastructure.Data;

public class ScheduleDbContextFactory : IDesignTimeDbContextFactory<ScheduleDbContext>
{
    public ScheduleDbContext CreateDbContext(string[] args)
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                           .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                           .AddJsonFile($"appsettings.Localhost.json", optional: true, reloadOnChange: true)
                           .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ScheduleDbContext>();
        optionsBuilder.UseSqlServer(configurationRoot.GetSection(nameof(ScheduleDbContext))["ConnectionString"]);

        return new ScheduleDbContext(optionsBuilder.Options);
    }
}