namespace Notiflow.Lib.Auth.IOC
{
    /// <summary>
    /// Extension methods for setting up MVC services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Add jwt token for authentication and authorization
        /// </summary>
        /// <remarks>It should add the necessary information to the application settings file.</remarks>
        /// <param name="services">type of built-in service collection interface</param>
        /// <returns>type of built-in service collection interface</returns>
        /// <seealso cref="https://jwt.io/"/>
        /// <exception cref="ArgumentNullException">throw when the service provider cannot be built</exception>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            JwtTokenSetting jwtTokenSetting = default;
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<JwtTokenSetting>(configuration.GetRequiredSection(nameof(JwtTokenSetting)));
            services.TryAddSingleton<IJwtTokenSetting>(provider =>
            {
                jwtTokenSetting = provider.GetRequiredService<IOptions<JwtTokenSetting>>().Value;
                return jwtTokenSetting;
            });

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
                    ValidAudience = jwtTokenSetting.Audiences[0],
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
        /// Add claim service
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims"/>
        /// <returns>type of built-in service collection interface</returns>
        public static IServiceCollection AddClaimService(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IClaimService, ClaimManager>();

            return services;
        }
    }
}
