namespace Notiflow.Backoffice.Application;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(configure => configure.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddLibraries();

        return services;
    }

    private static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        return services.AddFluentDesignValidation();
    }
}
