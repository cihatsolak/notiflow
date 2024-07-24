namespace Notiflow.Backoffice.Application;

internal static class MassTransitBuilderExtensions
{
    internal static IServiceCollection AddMassTransit(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        RabbitMqClusterSetting clusterSetting = configuration.GetRequiredSection(nameof(RabbitMqClusterSetting)).Get<RabbitMqClusterSetting>();

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            //serviceCollectionBusConfigurator.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("backoffice", false)); //TODOs

            busConfigurator.AddConsumer<ScheduledTextMessageEventConsumer>();
            busConfigurator.AddConsumer<ScheduledNotificationEventConsumer>();
            busConfigurator.AddConsumer<ScheduledEmailEventConsumer>();

            busConfigurator.UsingRabbitMq((registrationContext, rabbitMqConfigurator) =>
            {
                rabbitMqConfigurator.Host(new Uri(clusterSetting.HostAddress), "/", hostConfigurator =>
                {
                    hostConfigurator.Username(clusterSetting.Username);
                    hostConfigurator.Password(clusterSetting.Password);

                    hostConfigurator.UseCluster(clusterConfigurator =>
                    {
                        clusterSetting.NodeAddresses.ForEach(nodeAddress =>
                        {
                            clusterConfigurator.Node(nodeAddress);
                        });
                    });
                });

                rabbitMqConfigurator.UseMessageRetry(retryCfg =>
                {
                    retryCfg.Interval(3, TimeSpan.FromSeconds(10));
                });

                //1 dk içerisinde 1000 request yapabilecek şekilde sınırlandırılmıştır.
                rabbitMqConfigurator.UseRateLimit(1000, TimeSpan.FromMinutes(1));
                
                ConfigureQueues(registrationContext, rabbitMqConfigurator);
                //rabbitMqBusFactoryConfigurator.ConfigureEndpoints(busRegistrationContext); //TODOs
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
