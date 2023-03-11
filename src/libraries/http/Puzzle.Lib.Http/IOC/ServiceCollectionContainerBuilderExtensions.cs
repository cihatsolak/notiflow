namespace Puzzle.Lib.Http.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Add rest api service
        /// </summary>
        /// <remarks>it is a service that contains methods suitable for rest api architecture.</remarks>
        /// <see cref="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests"/>
        /// <param name="services">type of built-in service collection interface</param>
        /// <returns>type of built-in service collection interface</returns>
        public static IServiceCollection AddRestApiService(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.TryAddScoped<IHttpService, HttpManager>();

            return services;
        }
    }
}
