namespace Notiflow.Projections.TextMessageService;

internal static class ServiceCollectionBuilderExtensions
{
    internal static IServiceCollection AddCustomMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        RabbitMqStandaloneSetting rabbitMqStandaloneSetting = configuration.GetRequiredSection(nameof(RabbitMqStandaloneSetting)).Get<RabbitMqStandaloneSetting>();

        services.AddMassTransit(serviceCollectionBusConfigurator =>
        {
            serviceCollectionBusConfigurator.SetKebabCaseEndpointNameFormatter();

            serviceCollectionBusConfigurator.AddConsumer<TextMessageDeliveredEventConsumer>();
            serviceCollectionBusConfigurator.AddConsumer<TextMessageNotDeliveredEventConsumer>();

            serviceCollectionBusConfigurator.UsingRabbitMq((busRegistrationContext, rabbitMqBusFactoryConfigurator) =>
            {
                rabbitMqBusFactoryConfigurator.Host(new Uri(rabbitMqStandaloneSetting.HostAddress), "/", hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitMqStandaloneSetting.Username);
                    hostConfigurator.Password(rabbitMqStandaloneSetting.Password);
                });

                rabbitMqBusFactoryConfigurator.UseMessageRetry(retryCfg =>
                {
                    retryCfg.Interval(3, TimeSpan.FromSeconds(10));
                });

                //1 dk içerisinde 1000 request yapabilecek şekilde sınırlandırılmıştır.
                rabbitMqBusFactoryConfigurator.UseRateLimit(1000, TimeSpan.FromMinutes(1));

                rabbitMqBusFactoryConfigurator.ReceiveEndpoint(RabbitQueueName.TEXT_MESSAGE_DELIVERED_EVENT_QUEUE, options =>
                {
                    options.ConfigureConsumer<TextMessageDeliveredEventConsumer>(busRegistrationContext);
                });

                rabbitMqBusFactoryConfigurator.ReceiveEndpoint(RabbitQueueName.TEXT_MESSAGE_NOT_DELIVERED_EVENT_QUEUE, options =>
                {
                    options.ConfigureConsumer<TextMessageNotDeliveredEventConsumer>(busRegistrationContext);
                });
            });
        });

        return services;
    }
}
