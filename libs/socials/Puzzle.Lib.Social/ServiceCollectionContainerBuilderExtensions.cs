namespace Puzzle.Lib.Social;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddSocialServices(this IServiceCollection services , Action<SocialSettings> setup)
    {
        SocialSettings socialSettings = new();
        setup?.Invoke(socialSettings);

        services.Configure(setup);
        services.TryAddSingleton<IGoogleAuthService, GoogleAuthService>();
        services.TryAddSingleton<IFacebookAuthService, FacebookAuthService>();
        
        services.AddHttpClient(nameof(FacebookAuthService), httpClient =>
        {
            httpClient.BaseAddress = new Uri(socialSettings.FacebookAuthConfig.BaseUrl);
        });

        return services;
    }
}