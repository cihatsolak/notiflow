namespace Puzzle.Lib.Social;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddSocialServices(this IServiceCollection services , Action<SocialSettings> configure)
    {
        SocialSettings socialSettings = new();
        configure?.Invoke(socialSettings);

        services.Configure(configure);
        services.TryAddSingleton<IGoogleAuthService, GoogleAuthService>();
        services.TryAddSingleton<IFacebookAuthService, FacebookAuthService>();
        
        services.AddHttpClient(nameof(FacebookAuthService), httpClient =>
        {
            httpClient.BaseAddress = new Uri(socialSettings.FacebookAuthConfig.BaseUrl);
        });

        return services;
    }
}