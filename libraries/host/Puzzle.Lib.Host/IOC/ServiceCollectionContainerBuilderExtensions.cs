﻿namespace Puzzle.Lib.Host.IOC
{
    /// <summary>
    /// This class provides a set of extension methods for adding additional functionality to the IServiceCollection and ContainerBuilder instances.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Adds route settings to the provided IServiceCollection instance by configuring the routing options.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to which the route settings are added.</param>
        /// <returns>The modified IServiceCollection instance.</returns>
        public static IServiceCollection AddRouteSettings(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            return services;
        }

        /// <summary>
        /// Adds a controller with an authorization filter to the provided IServiceCollection instance.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to which the controller and filter are added.</param>
        /// <returns>The modified IServiceCollection instance.</returns>
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

            //Todo: should be examined

            return services;
        }
    }
}