namespace Puzzle.Lib.Security;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to add request detection functionality.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// Adds request detection functionality to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the request detection functionality to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddRequestDetection(this IServiceCollection services)
    {
        return services.AddDetection().Services;
    }

    /// <summary>
    /// Configures the services to handle forwarded headers in the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddForwardedHeaders(this IServiceCollection services)
    {
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        return services;
    }

    /// <summary>
    /// Adds an encryption service to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the encryption service will be added.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAesCipherService(this IServiceCollection services, Action<AesCipherSetting> configure)
    {
        services.Configure(configure);
        services.TryAddSingleton<IAesCipherService, AesCipherManager>();

        return services;
    }

    /// <summary>
    /// An IServiceCollection extension that enables the HTTP Strict Transport Security (HSTS) feature.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The IServiceCollection instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <see cref="IServiceProvider"/> instance obtained from the specified <see cref="IServiceCollection"/> is null.</exception>
    public static IServiceCollection AddHttpSecurityPrecautions(this IServiceCollection services, IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsProduction())
            return services;

        services.AddHttpsRedirection(options =>
        {
            options.HttpsPort = 443;
        });

        services.AddHsts(options =>
        {
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.MaxValue;
        });

        return services;
    }

    /// <summary>
    /// An IServiceCollection extension that adds a data protection service for the application and registers an implementation of IProtectorService.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The IServiceCollection instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <see cref="IServiceProvider"/> instance obtained from the specified <see cref="IServiceCollection"/> is null.</exception>
    public static IServiceCollection AddMicrosoftProtectorService(this IServiceCollection services, Action<RedisProtectorSetting> configure)
    {
        RedisProtectorSetting redisProtectorSetting = new();
        configure?.Invoke(redisProtectorSetting);

        IDatabase database = ConnectionMultiplexer.Connect(redisProtectorSetting.ConnectionString).GetDatabase(redisProtectorSetting.DatabaseNumber);

        services.AddDataProtection()
            .SetApplicationName(Assembly.GetCallingAssembly().FullName)
            .SetDefaultKeyLifetime(TimeSpan.FromDays(redisProtectorSetting.ExpirationDays))
            .PersistKeysToStackExchangeRedis(() => database, redisProtectorSetting.Key);

        services.TryAddSingleton<IMicrosoftProtectorService, MicrosoftProtectorManager>();

        return services;
    }

    /// <summary>
    /// Configures a CORS policy for an ASP.NET Core application using the IServiceCollection interface. 
    /// This allows cross-origin requests from any origin, with any HTTP method and header.
    /// </summary>
    /// <param name="services">The IServiceCollection interface used to register services with the application's dependency injection container.</param>
    /// <returns>The IServiceCollection instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <see cref="IServiceProvider"/> instance obtained from the specified <see cref="IServiceCollection"/> is null.</exception>
    public static IServiceCollection AddCustomCors(this IServiceCollection services, params string[] origins)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(Assembly.GetCallingAssembly().GetName().Name, builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins(origins));
        });

        return services;
    }
}