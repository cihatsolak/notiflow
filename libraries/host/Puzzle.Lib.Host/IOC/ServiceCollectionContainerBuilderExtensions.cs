namespace Puzzle.Lib.Host.IOC
{
    /// <summary>
    /// This class provides a set of extension methods for adding additional functionality to the IServiceCollection and ContainerBuilder instances.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Adds tag helper initializers for the ScriptTagHelper, LinkTagHelper, and ImageTagHelper classes to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the tag helper initializers to.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>

        public static IServiceCollection AddTagHelperInitializers(this IServiceCollection services)
        {
            services.AddSingleton<ITagHelperInitializer<ScriptTagHelper>, ScriptVersionTagHelperInitializer>();
            services.AddSingleton<ITagHelperInitializer<LinkTagHelper>, StyleVersionTagHelperInitializer>();
            services.AddSingleton<ITagHelperInitializer<ImageTagHelper>, ImageVersionTagHelperInitializer>();

            return services;
        }

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
    }
}
