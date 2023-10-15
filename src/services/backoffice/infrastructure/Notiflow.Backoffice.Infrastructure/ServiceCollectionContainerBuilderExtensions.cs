namespace Notiflow.Backoffice.Infrastructure;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.TryAddSingleton<IEmailService, EmailManager>();
        services.TryAddSingleton<ITextMessageService, TextMessageManager>();

        services
            .AddLowercaseRouting()
            .AddRestApiService()
            .AddFirebaseService(configuration)
            .AddHuaweiService(configuration);

        return services;
    }

    private static IServiceCollection AddFirebaseService(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(FirebaseSetting));
        services.Configure<FirebaseSetting>(configurationSection);
        FirebaseSetting firebaseSetting = configurationSection.Get<FirebaseSetting>();

        services.AddHttpClient(nameof(FirebaseManager), httpClient =>
        {
            httpClient.BaseAddress = firebaseSetting.BaseAddress;
            httpClient.Timeout = TimeSpan.FromSeconds(15);
            httpClient.DefaultRequestHeaders.ExpectContinue = false;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        });

        services.TryAddSingleton<IFirebaseService, FirebaseManager>();

        return services;
    }

    private static IServiceCollection AddHuaweiService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HuaweiSetting>(configuration.GetRequiredSection(nameof(HuaweiSetting)));

        services.AddHttpClient(nameof(HuaweiManager), httpClient =>
        {
            httpClient.Timeout = TimeSpan.FromSeconds(15);
            httpClient.DefaultRequestHeaders.ExpectContinue = false;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        });

        services.TryAddSingleton<IHuaweiService, HuaweiManager>();

        return services;
    }
}
