namespace Notiflow.Projections.NotificationService;

internal static class ServiceCollectionContainerBuilderExtensions
{
    internal static IServiceCollection AddNotiflowDbSetting(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

        return services.Configure<NotiflowDbSetting>(configuration.GetRequiredSection(nameof(NotiflowDbSetting)));
    }

    internal static IServiceCollection AddCustomMassTransit(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        RabbitMqStandaloneSetting rabbitMqStandaloneSetting = configuration.GetRequiredSection(nameof(RabbitMqStandaloneSetting)).Get<RabbitMqStandaloneSetting>();

        services.AddMassTransit(serviceCollectionBusConfigurator =>
        {
            serviceCollectionBusConfigurator.SetKebabCaseEndpointNameFormatter();

            serviceCollectionBusConfigurator.AddConsumer<NotificationDeliveredEventConsumer>();
            serviceCollectionBusConfigurator.AddConsumer<NotificationNotDeliveredEventConsumer>();

            serviceCollectionBusConfigurator.UsingRabbitMq((busRegistrationContext, rabbitMqBusFactoryConfigurator) =>
            {
                rabbitMqBusFactoryConfigurator.Host(new Uri(rabbitMqStandaloneSetting.HostAddress), "/", hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitMqStandaloneSetting.Username);
                    hostConfigurator.Password(rabbitMqStandaloneSetting.Password);
                });

                rabbitMqBusFactoryConfigurator.ReceiveEndpoint(RabbitQueueName.NOTIFICATION_DELIVERED_EVENT_QUEUE, options =>
                {
                    options.ConfigureConsumer<NotificationDeliveredEventConsumer>(busRegistrationContext);
                });

                rabbitMqBusFactoryConfigurator.ReceiveEndpoint(RabbitQueueName.NOTIFICATION_NOT_DELIVERED_EVENT_QUEUE, options =>
                {
                    options.ConfigureConsumer<NotificationNotDeliveredEventConsumer>(busRegistrationContext);
                });
            });
        });

        return services;
    }
}
