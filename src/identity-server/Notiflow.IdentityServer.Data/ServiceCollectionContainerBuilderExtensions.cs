using Puzzle.Lib.Database;

namespace Notiflow.IdentityServer.Data;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddDataDependencies(this IServiceCollection services)
    {
        services.AddMicrosoftSql<ApplicationDbContext>(nameof(ApplicationDbContext));
        services.SeedAsync().Wait();
       
        return services;
    }
}
