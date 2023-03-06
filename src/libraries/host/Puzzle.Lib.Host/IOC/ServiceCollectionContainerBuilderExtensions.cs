namespace Puzzle.Lib.Host.IOC
{
    /// <summary>
    /// Extension methods for setting up MVC services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Add route settings
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.routing.routeoptions.lowercaseurls?view=aspnetcore-6.0"/>
        /// <returns>type of built-in service collection interface</returns>
        public static IServiceCollection AddRouteSettings(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            return services;
        }

        /// <summary>
        /// Add controller with authorize filter
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.authorization.authorizefilter?view=aspnetcore-6.0"/>
        /// <returns>type of built-in service collection interface</returns>
        public static IServiceCollection AddControllerWithAuthorizeFilter(this IServiceCollection services)
        {
            //var authorizationPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //services.AddControllers(options =>
            //{
            //    options.Filters.Add(new AuthorizeFilter(authorizationPolicy));
            //}).AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            //    options.JsonSerializerOptions.WriteIndented = true;
            //});

            return services;
        }
    }
}
