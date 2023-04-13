namespace Notiflow.Backoffice.Infrastructure;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IFirebaseService, FirebaseManager>();

        return services;
    }
}
