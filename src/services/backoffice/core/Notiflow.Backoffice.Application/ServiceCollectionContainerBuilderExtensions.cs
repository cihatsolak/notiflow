using MassTransit;

namespace Notiflow.Backoffice.Application;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            opt.BehaviorsToRegister.AddRange(new List<ServiceDescriptor>
            {
                new(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), ServiceLifetime.Transient),
                new(typeof(IPipelineBehavior<,>), typeof(LanguageBehaviour<,>), ServiceLifetime.Singleton),
                new(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>), ServiceLifetime.Transient),
            });
        });

        services.AddFluentDesignValidation();
        services.AddRedisService();

        services.AddMassTransit();
        services.AddLocalization();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("tr-TR")
            };

            options.DefaultRequestCulture = new("tr-TR");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.ApplyCurrentCultureToResponseHeaders = true;
        });

        services.AddScoped<IClaimsTransformation, TenantIdClaimsTransformation>();

        services.AddSingleton<IAuthorizationHandler, MessagePermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, NotificationPermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, EmailPermissionAuthorizationHandler>();

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
