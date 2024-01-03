﻿namespace Notiflow.IdentityServer.Data.Seeds;

internal static class SeedManager
{
    internal static async Task SeedAsync(this IServiceCollection services, CancellationToken cancellationToken)
    {
		try
		{
            var applicationDbContext = services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();

            bool isConnected = await applicationDbContext.Database.CanConnectAsync(cancellationToken);
            if (!isConnected)
            {
                Debug.WriteLine("[SeedManager] Could not connect to database. The database may not be available.");
                return;
            }

            bool isExists = await applicationDbContext.Tenants.AnyAsync(cancellationToken);
            if (isExists)
            {
                Debug.WriteLine("[SeedManager] There is no need for migration as there is tenant information in the database.");
                return;
            }

            await applicationDbContext.Tenants.AddRangeAsync(SeedData.GenerateFakeTenants(), cancellationToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
		catch (Exception ex)
		{
            Debug.WriteLine($"[SeedManager] : {ex.Message}");
        }
    }
}
