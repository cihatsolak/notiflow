namespace Notiflow.Schedule;

internal static class MassTransitContainerBuilderExtensions
{
    internal static IServiceCollection AddMassTransit(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        RabbitMqClusterSetting rabbitMqClusterSetting = configuration.GetRequiredSection(nameof(RabbitMqClusterSetting)).Get<RabbitMqClusterSetting>();

        services.AddMassTransit(serviceCollectionBusConfigurator =>
        {
            serviceCollectionBusConfigurator.SetKebabCaseEndpointNameFormatter();

            serviceCollectionBusConfigurator.AddRequestClient<ScheduledTextMessageEvent>(new Uri($"{rabbitMqClusterSetting.HostAddress}/{RabbitQueueName.SCHEDULED_TEXT_MESSAGE_SEND}"), RequestTimeout.Default);
            serviceCollectionBusConfigurator.AddRequestClient<ScheduledNotificationEvent>(new Uri($"{rabbitMqClusterSetting.HostAddress}/{RabbitQueueName.SCHEDULED_NOTIFICATIN_SEND}"), RequestTimeout.Default);
            serviceCollectionBusConfigurator.AddRequestClient<ScheduledEmailEvent>(new Uri($"{rabbitMqClusterSetting.HostAddress}/{RabbitQueueName.SCHEDULED_EMAIL_SEND}"), RequestTimeout.Default);

            serviceCollectionBusConfigurator.UsingRabbitMq((busRegistrationContext, rabbitMqBusFactoryConfigurator) =>
            {
                rabbitMqBusFactoryConfigurator.Host(new Uri(rabbitMqClusterSetting.HostAddress), "/", hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitMqClusterSetting.Username);
                    hostConfigurator.Password(rabbitMqClusterSetting.Password);

                    hostConfigurator.UseCluster(rabbitMQClusterConfigurator =>
                    {
                        rabbitMqClusterSetting.NodeAddresses.ForEach(nodeAddress =>
                        {
                            rabbitMQClusterConfigurator.Node(nodeAddress);
                        });
                    });
                });
            });
        });

        return services;
    }
}
