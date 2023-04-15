namespace Notiflow.Backoffice.Persistence.Seeds
{
    internal static class SeedManager
    {
        internal static async Task SeedAsync(NotiflowDbContext notiflowDbContext)
        {
            if (notiflowDbContext.Tenants.Any())
            {
                return;
            }

            await Task.WhenAll(
                notiflowDbContext.Tenants.AddRangeAsync(TenantSeedData.GenerateFakeTenants()),
                notiflowDbContext.SaveChangesAsync()
                );
        }
    }
}
