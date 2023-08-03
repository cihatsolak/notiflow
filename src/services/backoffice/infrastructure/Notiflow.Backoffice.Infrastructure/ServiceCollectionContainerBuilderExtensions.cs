namespace Notiflow.Backoffice.Infrastructure;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddFirebase()
            .AddHuawei();

        services.TryAddSingleton<IEmailService, EmailManager>();
        services.TryAddSingleton<ITextMessageService, TextMessageManager>();

        services.AddMultiLanguage();

        return services.AddLibraries();
    }

    private static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        services
            .AddRouteSettings()
            .AddRestApiService();

        return services;
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
            configure.BaseAddress = firebaseSetting.BaseAddress;
        });

        return services.AddSingleton<IFirebaseService, FirebaseManager>();
    }

    private static IServiceCollection AddHuawei(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(FirebaseSetting));
        services.Configure<FirebaseSetting>(configurationSection);
        FirebaseSetting firebaseSetting = configurationSection.Get<FirebaseSetting>();

        services.AddHttpClient("huawei", configure =>
        {
            configure.BaseAddress = firebaseSetting.BaseAddress;
        });

        return services.AddSingleton<IHuaweiService, HuaweiManager>();
    }

    private static IServiceCollection AddMultiLanguage(this IServiceCollection services)
    {
        services.AddLocalization(options =>
        {
            options.ResourcesPath = "Resources";
        });

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
