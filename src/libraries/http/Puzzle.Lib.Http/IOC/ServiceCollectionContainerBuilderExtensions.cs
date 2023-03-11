namespace Puzzle.Lib.Http.IOC
{
    /// <summary>
    /// Extension methods for setting up MVC services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Add custom http client service
        /// </summary>
        /// <remarks>it is a service that contains methods suitable for rest api architecture.</remarks>
        /// <see cref="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests"/>
        /// <param name="services">type of built-in service collection interface</param>
        /// <returns>type of built-in service collection interface</returns>
        public static IServiceCollection AddRestApiService(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.TryAddSingleton<IHttpService, HttpManager>();

            return services;
        }
    }
}
