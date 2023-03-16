namespace Puzzle.Lib.MessageBroker.IOC
{
    /// <summary>
    /// Contains extension methods for the IServiceCollection interface related to adding RabbitMQ settings.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Adds RabbitMQ settings to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the RabbitMQ settings to.</param>
        /// <returns>The updated IServiceCollection instance.</returns>
        public static IServiceCollection AddRabbitMqSetting(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<RabbitMqSetting>(configuration.GetRequiredSection(nameof(RabbitMqSetting)));
            
            return services;
        }
    }
}
