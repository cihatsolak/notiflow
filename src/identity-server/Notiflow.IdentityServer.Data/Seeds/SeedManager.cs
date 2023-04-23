namespace Notiflow.IdentityServer.Data.Seeds;

internal static class SeedManager
{
    internal static async Task SeedAsync(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

        if (!applicationDbContext.Database.CanConnect())
        {
            Debug.WriteLine("Could not connect to database. The database may not be available.");
            return;
        }

        if (applicationDbContext.Tenants.Any())
        {
            Debug.WriteLine("There is no need for migration as there is tenant information in the database.");
            return;
        }

        await applicationDbContext.Tenants.AddRangeAsync(TenantSeedData.GenerateFakeTenants());
        await applicationDbContext.SaveChangesAsync();
    }
}
