using MassTransit;

namespace Notiflow.Backoffice.Application;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(configure => configure.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LanguageBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        services.AddFluentDesignValidation();
        services.AddRedisService();

        services.AddMassTransit();
        services.AddMultiLanguage();

        services.AddScoped<IClaimsTransformation, TenantIdClaimsTransformation>();
        services.AddScoped<IAuthorizationHandler, MessagePermissionAuthorizationHandler>();

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

    private static IServiceCollection AddMultiLanguage(this IServiceCollection services)
    {
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

        return services;
    }
}
