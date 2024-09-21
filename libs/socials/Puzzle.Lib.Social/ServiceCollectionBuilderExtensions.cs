namespace Puzzle.Lib.Social;

/// <summary>
/// Extensions for IServiceCollection to add social authentication services.
/// </summary>
public static class ServiceCollectionBuilderExtensions
{
    /// <summary>
    /// Adds social authentication services to the IServiceCollection with the provided configuration.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="configure">An action to configure the SocialSettings.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddSocialServices(this IServiceCollection services, Action<SocialSettings> configure)
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
