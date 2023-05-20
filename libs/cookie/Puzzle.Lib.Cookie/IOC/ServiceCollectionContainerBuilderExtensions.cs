namespace Puzzle.Lib.Cookie.IOC
{
    /// <summary>
    /// Provides extension methods for configuring cookie authentication and cookie policy for an IServiceCollection.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Adds cookie authentication to the IServiceCollection using the specified configuration settings.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to add the authentication services to.</param>
        /// <returns>The IServiceCollection instance with the authentication services added.</returns>
        public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(CookieAuthenticationSetting));
            services.Configure<CookieAuthenticationSetting>(configurationSection);
            CookieAuthenticationSetting cookieAuthenticationSetting = configurationSection.Get<CookieAuthenticationSetting>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
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


            return services;
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

        /// <summary>
        /// Adds a cookie service to the IServiceCollection for managing cookies.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to add the cookie service to.</param>
        /// <returns>The IServiceCollection instance with the cookie service added.</returns>
        public static IServiceCollection AddCookieService(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<ICookieService, CookieManager>();

            return services;
        }
    }
}
