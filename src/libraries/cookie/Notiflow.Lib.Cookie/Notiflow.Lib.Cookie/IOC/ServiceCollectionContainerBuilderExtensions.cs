namespace Notiflow.Lib.Cookie.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Add cookie authentication
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <returns>type of built-in service collection interface</returns>
        /// <seealso cref="https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-7.0"/>
        /// <exception cref="ArgumentNullException">thrown when the service provider cannot be built</exception>
        public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            CookieAuthenticationSetting cookieAuthenticationSetting = default;
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<CookieAuthenticationSetting>(configuration.GetRequiredSection(nameof(CookieAuthenticationSetting)));
            services.TryAddSingleton<ICookieAuthenticationSetting>(provider =>
            {
                cookieAuthenticationSetting = provider.GetRequiredService<IOptions<CookieAuthenticationSetting>>().Value;
                return cookieAuthenticationSetting;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configure =>
                    {
                        configure.LoginPath = new(cookieAuthenticationSetting.LoginPath);
                        configure.LogoutPath = new(cookieAuthenticationSetting.LogoutPath);
                        configure.AccessDeniedPath = new PathString(cookieAuthenticationSetting.AccessDeniedPath);
                        configure.ExpireTimeSpan = TimeSpan.FromHours(cookieAuthenticationSetting.ExpireHour);
                        configure.SlidingExpiration = false;
                        configure.Cookie = new()
                        {
                            Name = Assembly.GetEntryAssembly().GetName().Name.ToLower(CultureInfo.InvariantCulture),
                            HttpOnly = true,
                            SameSite = SameSiteMode.Strict,
                            SecurePolicy = CookieSecurePolicy.Always
                        };
                    });


            return services;
        }

        /// <summary>
        /// Add cookie policy
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <returns>type of built-in service collection interface</returns>
        public static IServiceCollection ConfigureCookiePolicy(this IServiceCollection services)
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
        /// Add cookie service
        /// </summary>
        /// <returns>type of built-in service collection interface</returns>
        public static IServiceCollection AddCookieService(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<ICookieService, CookieManager>();

            return services;
        }
    }
}
