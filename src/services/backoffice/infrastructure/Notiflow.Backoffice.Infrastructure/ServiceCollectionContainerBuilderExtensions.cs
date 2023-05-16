﻿namespace Notiflow.Backoffice.Infrastructure;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddFirebase()
            .AddHuawei();

        services.TryAddSingleton<IEmailService, EmailManager>();
        services.TryAddSingleton<ITextMessageService, TextMessageManager>();

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
}
