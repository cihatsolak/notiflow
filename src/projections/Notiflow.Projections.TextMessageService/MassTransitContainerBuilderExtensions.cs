﻿namespace Notiflow.Projections.TextMessageService;

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

            serviceCollectionBusConfigurator.AddConsumer<TextMessageDeliveredEventConsumer>();
            serviceCollectionBusConfigurator.AddConsumer<TextMessageNotDeliveredEventConsumer>();

            serviceCollectionBusConfigurator.UsingRabbitMq((busRegistrationContext, rabbitMqBusFactoryConfigurator) =>
            {
                rabbitMqBusFactoryConfigurator.Host(new Uri(rabbitMqStandaloneSetting.HostAddress), "/", hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitMqStandaloneSetting.Username);
                    hostConfigurator.Password(rabbitMqStandaloneSetting.Password);
                });

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