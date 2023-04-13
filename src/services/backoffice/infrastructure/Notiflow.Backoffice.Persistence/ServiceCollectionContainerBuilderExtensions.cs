namespace Notiflow.Backoffice.Persistence;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext();
        services.AddRepositories();

        return services;
    }

    private static void AddDbContext(this IServiceCollection services)
    {
        services.AddPostgreSql<NotiflowDbContext>(nameof(NotiflowDbContext));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
    }
}
