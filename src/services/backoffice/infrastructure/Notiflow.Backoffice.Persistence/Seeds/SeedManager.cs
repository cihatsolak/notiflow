﻿namespace Notiflow.Backoffice.Persistence.Seeds;

internal static class SeedManager
{
    internal static async Task SeedAsync(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var notiflowDbContext = serviceProvider.GetRequiredService<NotiflowDbContext>();

        if (!notiflowDbContext.Database.CanConnect())
        {
            Debug.WriteLine("Could not connect to database. The database may not be available.");
            return;
        }

        if (notiflowDbContext.Tenants.Any())
        {
            Debug.WriteLine("There is no need for migration as there is tenant information in the database.");
            return;
        }

        await Task.WhenAll(notiflowDbContext.Tenants.AddRangeAsync(TenantSeedData.GenerateFakeTenants()), notiflowDbContext.SaveChangesAsync());
    }
}