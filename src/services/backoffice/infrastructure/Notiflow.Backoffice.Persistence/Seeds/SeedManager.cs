namespace Notiflow.Backoffice.Persistence.Seeds;

internal static class SeedManager
{
    internal static async Task SeedAsync(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var notiflowDbContext = serviceProvider.GetRequiredService<NotiflowDbContext>();

        bool isConnected = await notiflowDbContext.Database.CanConnectAsync(CancellationToken.None);
        if (!isConnected)
        {
            Debug.WriteLine("Could not connect to database. The database may not be available.");
            return;
        }

        bool isExists = await notiflowDbContext.Customers.IgnoreQueryFilters().AnyAsync();
        if (isExists)
        {
            Debug.WriteLine("There is no need for migration as there is tenant information in the database.");
            return;
        }

        await notiflowDbContext.Customers.AddRangeAsync(SeedData.GenerateCustomers());
        await notiflowDbContext.SaveChangesAsync(CancellationToken.None);
    }
}
