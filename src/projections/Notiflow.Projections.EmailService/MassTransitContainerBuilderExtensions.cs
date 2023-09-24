namespace Notiflow.Projections.EmailService;

internal static class MassTransitContainerBuilderExtensions
{
    internal static IServiceCollection AddCustomMassTransit(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
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
