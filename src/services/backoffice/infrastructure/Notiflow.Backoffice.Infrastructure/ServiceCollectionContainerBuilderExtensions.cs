namespace Notiflow.Backoffice.Infrastructure;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddLibraries();

        services.AddSingleton<IFirebaseService, FirebaseManager>();

        return services;
    }

    private static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        services.AddRouteSettings();

        return services.AddRestApiService();
    }
}
