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
        RabbitMqSetting rabbitMqSetting = configuration.GetRequiredSection(nameof(RabbitMqSetting)).Get<RabbitMqSetting>();

        services.AddMassTransit(serviceCollectionBusConfigurator =>
        {
            serviceCollectionBusConfigurator.AddConsumer<NotificationDeliveredEventConsumer>();
            serviceCollectionBusConfigurator.AddConsumer<NotificationNotDeliveredEventConsumer>();

            serviceCollectionBusConfigurator.UsingRabbitMq((busRegistrationContext, rabbitMqBusFactoryConfigurator) =>
            {
                rabbitMqBusFactoryConfigurator.Host(rabbitMqSetting.ConnectionString, "/", hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitMqSetting.Username);
                    hostConfigurator.Password(rabbitMqSetting.Password);
                });

                rabbitMqBusFactoryConfigurator.ReceiveEndpoint(SharedRabbitQueueName.NOTIFICATION_DELIVERED_EVENT_QUEUE, options =>
                {
                    options.ConfigureConsumer<NotificationDeliveredEventConsumer>(busRegistrationContext);
                });

                rabbitMqBusFactoryConfigurator.ReceiveEndpoint(SharedRabbitQueueName.NOTIFICATION_NOT_DELIVERED_EVENT_QUEUE, options =>
                {
                    options.ConfigureConsumer<NotificationNotDeliveredEventConsumer>(busRegistrationContext);
                });
            });
        });

        return services;
    }
}
