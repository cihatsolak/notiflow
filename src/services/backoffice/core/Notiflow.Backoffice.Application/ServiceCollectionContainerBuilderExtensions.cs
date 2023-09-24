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
                new(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>), ServiceLifetime.Singleton),
                new(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), ServiceLifetime.Scoped),
                new(typeof(IPipelineBehavior<,>), typeof(LanguageBehaviour<,>), ServiceLifetime.Singleton),
                new(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>), ServiceLifetime.Scoped),
                new(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>), ServiceLifetime.Scoped),
            });
        });

        services.AddFluentDesignValidation();
        services.AddRedisService();

        services.AddHttpContextAccessor();

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
}
