namespace Notiflow.Backoffice.Application;

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

            serviceCollectionBusConfigurator.AddConsumer<ScheduledTextMessageEventConsumer>();
            serviceCollectionBusConfigurator.AddConsumer<ScheduledNotificationEventConsumer>();

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

                rabbitMqBusFactoryConfigurator.ReceiveEndpoint(RabbitQueueName.SCHEDULED_TEXT_MESSAGE_SEND, options =>
                {
                    options.ConfigureConsumer<ScheduledTextMessageEventConsumer>(busRegistrationContext);
                });

                rabbitMqBusFactoryConfigurator.ReceiveEndpoint(RabbitQueueName.SCHEDULED_NOTIFICATIN_SEND, options =>
                {
                    options.ConfigureConsumer<ScheduledNotificationEventConsumer>(busRegistrationContext);
                });
            });
        });

        return services;
    }
}
