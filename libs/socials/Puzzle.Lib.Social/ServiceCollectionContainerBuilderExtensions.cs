namespace Puzzle.Lib.Social;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddSocialServices(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(SocialSettings));
        services.Configure<SocialSettings>(configurationSection);

        SocialSettings socialSettings = configurationSection.Get<SocialSettings>();

        services.TryAddSingleton<IGoogleAuthService, GoogleAuthService>();
        services.TryAddSingleton<IFacebookAuthService, FacebookAuthService>();
        
        services.AddHttpClient(nameof(FacebookAuthService), httpClient =>
        {
            httpClient.BaseAddress = new Uri(socialSettings.FacebookAuthConfig.BaseUrl);
        });

        return services;
    }
}