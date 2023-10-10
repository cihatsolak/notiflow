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
            .AddFirebase(configuration)
            .AddHuawei(configuration);

        return services;
    }

    private static IServiceCollection AddFirebase(this IServiceCollection services, IConfiguration configuration)
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

    private static IServiceCollection AddHuawei(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(HuaweiSetting));
        services.Configure<HuaweiSetting>(configurationSection);
        HuaweiSetting huaweiSetting = configurationSection.Get<HuaweiSetting>();

        services.AddHttpClient(nameof(HuaweiManager), httpClient =>
        {
            httpClient.BaseAddress = huaweiSetting.BaseAddress;
            httpClient.Timeout = TimeSpan.FromSeconds(15);
            httpClient.DefaultRequestHeaders.ExpectContinue = false;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        });

        services.TryAddSingleton<IHuaweiService, HuaweiManager>();

        return services;
    }
}
