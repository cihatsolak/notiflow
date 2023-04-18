namespace Notiflow.Backoffice.Infrastructure;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddFirebase();
        services.AddLibraries();

        return services;
    }

    private static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        services.AddRouteSettings();

        return services.AddRestApiService();
    }

    private static IServiceCollection AddFirebase(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(FirebaseSetting));
        services.Configure<FirebaseSetting>(configurationSection);
        FirebaseSetting firebaseSetting = configurationSection.Get<FirebaseSetting>();

        services.AddHttpClient("firebase", configure =>
        {
            configure.BaseAddress = firebaseSetting.BaseAddress; //Todo
        });

        return services.AddSingleton<IFirebaseService, FirebaseManager>();
    }
}
