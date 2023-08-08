using MassTransit;
using Puzzle.Lib.Cache;
using Puzzle.Lib.Validation;

namespace Notiflow.Backoffice.Application;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(configure => configure.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddLibraries();

        services.AddMassTransit();

        return services;
    }

    private static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        services.AddFluentDesignValidation();
        services.AddRedisService();

        return services;
    }

    private static IServiceCollection AddMassTransit(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        RabbitMqSetting rabbitMqSetting = configuration.GetRequiredSection(nameof(RabbitMqSetting)).Get<RabbitMqSetting>();

        services.AddMassTransit(serviceCollectionBusConfigurator =>
        {
            serviceCollectionBusConfigurator.UsingRabbitMq((busRegistrationContext, rabbitMqBusFactoryConfigurator) =>
            {
                rabbitMqBusFactoryConfigurator.Host(rabbitMqSetting.ConnectionString, "/", hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitMqSetting.Username);
                    hostConfigurator.Password(rabbitMqSetting.Password);
                });
            });
        });

        return services;
    }
}
