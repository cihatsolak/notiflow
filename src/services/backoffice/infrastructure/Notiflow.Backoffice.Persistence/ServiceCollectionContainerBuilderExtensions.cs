namespace Notiflow.Backoffice.Persistence;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services
            .AddDbContext()
            .AddRepositories()
            .AddUnitOfWorks();

        services.SeedAsync().Wait();

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        return services.AddPostgreSql<NotiflowDbContext>(nameof(NotiflowDbContext));
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomerReadRepository, CustomerReadRepository>()
            .AddScoped<ICustomerWriteRepository, CustomerWriteRepository>()
            .AddScoped<ITenantReadRepository, TenantReadRepository>()
            .AddScoped<ITenantWriteRepository, TenantWriteRepository>()
            .AddScoped<IDeviceReadRepository, DeviceReadRepository>()
            .AddScoped<IDeviceWriteRepository, DeviceWriteRepository>();
    }

    private static IServiceCollection AddUnitOfWorks(this IServiceCollection services)
    {
        return services.AddScoped<INotiflowUnitOfWork, NotiflowUnitOfWork>();
    }
}
