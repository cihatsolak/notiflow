namespace Notiflow.Backoffice.Persistence.Contexts;

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

        return new NotiflowDbContext(optionsBuilder.Options, null);
    }
}