namespace Notiflow.Backoffice.Application;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        RedisServerSetting redisServerSetting = configuration.GetRequiredSection(nameof(RedisServerSetting)).Get<RedisServerSetting>();

        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            opt.BehaviorsToRegister.AddRange(new List<ServiceDescriptor>
            {
                new(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>), ServiceLifetime.Singleton),
                new(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), ServiceLifetime.Scoped),
                new(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>), ServiceLifetime.Scoped),
                new(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>), ServiceLifetime.Scoped),
            });
        });

        services.AddRedisService(options =>
        {
            options.ConnectionString = redisServerSetting.ConnectionString;
            options.AbortOnConnectFail = redisServerSetting.AbortOnConnectFail;
            options.AsyncTimeOutSecond = redisServerSetting.AsyncTimeOutSecond;
            options.ConnectTimeOutSecond = redisServerSetting.ConnectTimeOutSecond;
            options.Username = redisServerSetting.Username;
            options.Password = redisServerSetting.Password;
            options.DefaultDatabase = redisServerSetting.DefaultDatabase;
            options.AllowAdmin = redisServerSetting.AllowAdmin;
        });

        services
            .AddMassTransit()
            .AddWebApiLocalize()
            .AddHttpContextAccessor()
            .AddServerSideFluentValidation()
            .AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IClaimsTransformation, TenantIdClaimsTransformation>();

        services.AddSingleton<IAuthorizationHandler, MessagePermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, NotificationPermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, EmailPermissionAuthorizationHandler>();

        return services;
    }
}
