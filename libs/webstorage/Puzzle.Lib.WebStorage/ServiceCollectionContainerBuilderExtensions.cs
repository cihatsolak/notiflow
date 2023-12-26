namespace Puzzle.Lib.Cookie;

/// <summary>
/// Provides extension methods for configuring cookie authentication and cookie policy for an IServiceCollection.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// Adds cookie authentication to the IServiceCollection using the specified configuration settings.
    /// </summary>
    /// <param name="builder">The IServiceCollection instance to add the authentication services to.</param>
    /// <returns>The IServiceCollection instance with the authentication services added.</returns>
    public static IServiceCollection AddCookieAuthentication(this WebApplicationBuilder builder)
    {
        IConfigurationSection configurationSection = builder.Configuration.GetRequiredSection(nameof(CookieAuthenticationSetting));
        builder.Services.Configure<CookieAuthenticationSetting>(configurationSection);
        CookieAuthenticationSetting cookieAuthenticationSetting = configurationSection.Get<CookieAuthenticationSetting>();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configure =>
                {
                    configure.LoginPath = new(cookieAuthenticationSetting.LoginPath);
                    configure.LogoutPath = new(cookieAuthenticationSetting.LogoutPath);
                    configure.AccessDeniedPath = new(cookieAuthenticationSetting.AccessDeniedPath);
                    configure.ExpireTimeSpan = TimeSpan.FromHours(cookieAuthenticationSetting.ExpireHour);
                    configure.SlidingExpiration = false;
                    configure.Cookie = new()
                    {
                        Name = Assembly.GetEntryAssembly().GetName().Name.ToLowerInvariant(),
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict,
                        SecurePolicy = CookieSecurePolicy.Always
                    };
                });

        return builder.Services;
    }

    /// <summary>
    /// Configures the cookie policy options to enforce a strict secure policy.
    /// </summary>
    /// <param name="services">The IServiceCollection instance to configure the cookie policy options for.</param>
    /// <returns>The IServiceCollection instance with the cookie policy options configured.</returns>
    public static IServiceCollection ConfigureSecureCookiePolicy(this IServiceCollection services)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.Secure = CookieSecurePolicy.Always;
            options.HttpOnly = HttpOnlyPolicy.Always;
            options.MinimumSameSitePolicy = SameSiteMode.Strict;
        });

        return services;
    }
}