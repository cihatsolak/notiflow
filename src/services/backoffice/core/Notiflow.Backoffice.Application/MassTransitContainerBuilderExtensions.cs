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
            serviceCollectionBusConfigurator.AddConsumer<ScheduledEmailEventConsumer>();

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

                rabbitMqBusFactoryConfigurator.UseMessageRetry(retryCfg =>
                {
                    retryCfg.Interval(3, TimeSpan.FromSeconds(10));
                });

                //1 dk içerisinde 1000 request yapabilecek şekilde sınırlandırılmıştır.
                rabbitMqBusFactoryConfigurator.UseRateLimit(1000, TimeSpan.FromMinutes(1));

                ConfigureQueues(busRegistrationContext, rabbitMqBusFactoryConfigurator);
            });
        });
        return services;
    }

    private static void ConfigureQueues(IBusRegistrationContext busRegistrationContext, IRabbitMqBusFactoryConfigurator rabbitMqBusFactoryConfigurator)
    {
        rabbitMqBusFactoryConfigurator.ReceiveEndpoint(RabbitQueueName.SCHEDULED_TEXT_MESSAGE_SEND, options =>
        {
            options.ConfigureConsumer<ScheduledTextMessageEventConsumer>(busRegistrationContext);
        });

        rabbitMqBusFactoryConfigurator.ReceiveEndpoint(RabbitQueueName.SCHEDULED_NOTIFICATIN_SEND, options =>
        {
            options.ConfigureConsumer<ScheduledNotificationEventConsumer>(busRegistrationContext);
        });

        rabbitMqBusFactoryConfigurator.ReceiveEndpoint(RabbitQueueName.SCHEDULED_EMAIL_SEND, options =>
        {
            options.ConfigureConsumer<ScheduledEmailEventConsumer>(busRegistrationContext);
        });
    }
}
