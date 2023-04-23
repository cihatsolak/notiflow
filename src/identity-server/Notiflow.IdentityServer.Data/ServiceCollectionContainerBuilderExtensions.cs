using Microsoft.Extensions.DependencyInjection.Extensions;
using Puzzle.Lib.Database.Concrete;
using Puzzle.Lib.Database.Interfaces;

namespace Notiflow.IdentityServer.Data;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddMicrosoftSql<ApplicationDbContext>(nameof(ApplicationDbContext));
        services.SeedAsync().Wait();
       
        return services;
    }
}
