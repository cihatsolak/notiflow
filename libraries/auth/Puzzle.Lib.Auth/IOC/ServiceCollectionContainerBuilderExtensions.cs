namespace Puzzle.Lib.Auth.IOC
{
    /// <summary>
    /// Provides extension methods to add JWT authentication and claim services to the <see cref="IServiceCollection"/> container.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Adds JWT authentication services to the <see cref="IServiceCollection"/> container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(JwtTokenSetting));
            services.Configure<JwtTokenSetting>(configurationSection);
            JwtTokenSetting jwtTokenSetting = configurationSection.Get<JwtTokenSetting>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions =>
            {
                configureOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtTokenSetting.Issuer,
                    ValidAudience = jwtTokenSetting.Audiences.First(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSetting.SecurityKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

            return services;
        }

        /// <summary>
        /// Adds JWT authentication with events services to the <see cref="IServiceCollection"/> container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddJwtAuthenticationWithEvents(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(JwtTokenSetting));
            services.Configure<JwtTokenSetting>(configurationSection);
            JwtTokenSetting jwtTokenSetting = configurationSection.Get<JwtTokenSetting>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions =>
            {
                configureOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtTokenSetting.Issuer,
                    ValidAudience = jwtTokenSetting.Audiences.First(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSetting.SecurityKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };

                configureOptions.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        string token = context.Request.Query["access_token"];
                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        /// <summary>
        /// Adds claim services to the <see cref="IServiceCollection"/> container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddClaimService(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IClaimService, ClaimManager>();

            return services;
        }
    }
}
