namespace Notiflow.Schedule;

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

            busConfigurator.AddRequestClient<ScheduledTextMessageEvent>(new Uri($"{clusterSetting.HostAddress}/{RabbitQueueName.SCHEDULED_TEXT_MESSAGE_SEND}"), RequestTimeout.Default);
            busConfigurator.AddRequestClient<ScheduledNotificationEvent>(new Uri($"{clusterSetting.HostAddress}/{RabbitQueueName.SCHEDULED_NOTIFICATIN_SEND}"), RequestTimeout.Default);
            busConfigurator.AddRequestClient<ScheduledEmailEvent>(new Uri($"{clusterSetting.HostAddress}/{RabbitQueueName.SCHEDULED_EMAIL_SEND}"), RequestTimeout.Default);

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

                    rabbitMqConfigurator.UseMessageRetry(retryCfg =>
                    {
                        retryCfg.Interval(3, TimeSpan.FromSeconds(10));
                    });

                    //1 dk içerisinde 1000 request yapabilecek şekilde sınırlandırılmıştır.
                    rabbitMqConfigurator.UseRateLimit(1000, TimeSpan.FromMinutes(1));
                });
            });
        });

        return services;
    }
}
