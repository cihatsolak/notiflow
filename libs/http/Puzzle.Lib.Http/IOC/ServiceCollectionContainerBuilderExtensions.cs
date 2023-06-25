namespace Puzzle.Lib.Http.IOC;

/// <summary>
/// Provides extension methods to register an HTTP REST API service to the dependency injection container.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// Adds an HTTP REST API service to the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddRestApiService(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.TryAddSingleton<IRestService, RestManager>();

        return services;
    }
}
