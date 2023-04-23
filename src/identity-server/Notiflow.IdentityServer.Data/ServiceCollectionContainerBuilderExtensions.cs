namespace Notiflow.IdentityServer.Service;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        return services.AddMicrosoftSql<ApplicationDbContext>(nameof(ApplicationDbContext));
    }
}
