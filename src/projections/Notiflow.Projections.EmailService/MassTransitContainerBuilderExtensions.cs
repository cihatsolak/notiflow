namespace Notiflow.Projections.EmailService;

internal static class MassTransitContainerBuilderExtensions
{
    internal static IServiceCollection AddCustomMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        RabbitMqStandaloneSetting rabbitMqStandaloneSetting = configuration.GetRequiredSection(nameof(RabbitMqStandaloneSetting)).Get<RabbitMqStandaloneSetting>();

        services.AddMassTransit(serviceCollectionBusConfigurator =>
        {
            serviceCollectionBusConfigurator.SetKebabCaseEndpointNameFormatter();

            serviceCollectionBusConfigurator.AddConsumer<EmailDeliveredEventConsumer>();
            serviceCollectionBusConfigurator.AddConsumer<EmailNotDeliveredEventConsumer>();

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

                rabbitMqBusFactoryConfigurator.ReceiveEndpoint(RabbitQueueName.EMAIL_DELIVERED_EVENT_QUEUE, options =>
                {
                    options.ConfigureConsumer<EmailDeliveredEventConsumer>(busRegistrationContext);
                });

                rabbitMqBusFactoryConfigurator.ReceiveEndpoint(RabbitQueueName.EMAIL_NOT_DELIVERED_EVENT_QUEUE, options =>
                {
                    options.ConfigureConsumer<EmailNotDeliveredEventConsumer>(busRegistrationContext);
                });
            });
        });

        return services;
    }
}
