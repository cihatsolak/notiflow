namespace Notiflow.Projections.NotificationService;

internal static class ServiceCollectionContainerBuilderExtensions
{
    internal static IServiceCollection AddNotiflowDbSetting(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

        return services.Configure<NotiflowDbSetting>(configuration.GetRequiredSection(nameof(NotiflowDbSetting)));
    }

    internal static IServiceCollection AddMassTransit(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        RabbitMqSetting rabbitMqSetting = configuration.GetRequiredSection(nameof(RabbitMqSetting)).Get<RabbitMqSetting>();

        services.AddMassTransit(serviceCollectionBusConfigurator =>
        {
            serviceCollectionBusConfigurator.AddConsumer<TextMessageDeliveredEventConsumer>();
            serviceCollectionBusConfigurator.AddConsumer<TextMessageNotDeliveredEventConsumer>();

            serviceCollectionBusConfigurator.UsingRabbitMq((busRegistrationContext, rabbitMqBusFactoryConfigurator) =>
            {
                rabbitMqBusFactoryConfigurator.Host(rabbitMqSetting.ConnectionString, "/", hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitMqSetting.Username);
                    hostConfigurator.Password(rabbitMqSetting.Password);
                });

                rabbitMqBusFactoryConfigurator.ReceiveEndpoint("text-message-delivered-event-queue", options =>
                {
                    options.ConfigureConsumer<TextMessageDeliveredEventConsumer>(busRegistrationContext);
                });

                rabbitMqBusFactoryConfigurator.ReceiveEndpoint("text-message-not-delivered-event-queue", options =>
                {
                    options.ConfigureConsumer<TextMessageNotDeliveredEventConsumer>(busRegistrationContext);
                });
            });
        });

        return services;
    }
}
