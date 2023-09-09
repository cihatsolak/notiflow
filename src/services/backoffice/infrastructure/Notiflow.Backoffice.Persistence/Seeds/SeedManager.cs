namespace Notiflow.Backoffice.Persistence.Seeds;

internal static class SeedManager
{
    internal static async Task SeedAsync(this IServiceCollection services, CancellationToken cancellationToken = default)
    {
        EnsureNotNull(services);

        var notiflowDbContext = services.BuildServiceProvider().GetRequiredService<NotiflowDbContext>();

        bool isConnected = await notiflowDbContext.Database.CanConnectAsync(cancellationToken);
        if (!isConnected)
        {
            Debug.WriteLine("[SeedManager] Could not connect to database. The database may not be available.");
            return;
        }

        bool isExists = await notiflowDbContext.Customers.IgnoreQueryFilters().AnyAsync(cancellationToken);
        if (isExists)
        {
            Debug.WriteLine("[SeedManager] There is no need for migSration as there is tenant information in the database.");
            return;
        }

        try
        {
            await notiflowDbContext.Customers.AddRangeAsync(SeedData.GenerateCustomers(), cancellationToken);
            await notiflowDbContext.SaveChangesAsync(cancellationToken);
        }
        finally
        {
            await notiflowDbContext.DisposeAsync();
        }
    }

    private static void EnsureNotNull(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
    }
}
