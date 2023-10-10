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
    /// Adds protocol service functionality to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the protocol service functionality to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <see cref="IServiceProvider"/> instance obtained from the specified <see cref="IServiceCollection"/> is null.</exception>
    public static IServiceCollection AddProtocolService(this IServiceCollection services, Action<HostingSetting> configure)
    {
        services.Configure(configure);
        services.TryAddSingleton<IProtocolService, ProtocolManager>();

        return services;
    }

    /// <summary>
    /// Adds an encryption service to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the encryption service will be added.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddEncryptionService(this IServiceCollection services, Action<EncryptionSetting> configure)
    {
        services.Configure(configure);
        services.TryAddSingleton<IEncryptionService, EncryptionManager>();

        return services;
    }

    /// <summary>
    /// An IServiceCollection extension that enables the HTTP Strict Transport Security (HSTS) feature.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The IServiceCollection instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <see cref="IServiceProvider"/> instance obtained from the specified <see cref="IServiceCollection"/> is null.</exception>
    public static IServiceCollection AddHttpSecurityPrecautions(this IServiceCollection services, bool isProduction)
    {
        if (!isProduction)
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
    public static IServiceCollection AddProtectorServiceWithRedisStore(this IServiceCollection services, Action<RedisProtectorSetting> configure)
    {
        RedisProtectorSetting redisProtectorSetting = new();
        configure?.Invoke(redisProtectorSetting);

        services.AddDataProtection()
            .SetApplicationName(Assembly.GetCallingAssembly().FullName)
            .SetDefaultKeyLifetime(TimeSpan.FromDays(redisProtectorSetting.ExpirationDays))
            .PersistKeysToStackExchangeRedis(() => ConnectionMultiplexer.Connect(redisProtectorSetting.ConnectionString).GetDatabase(redisProtectorSetting.DatabaseNumber), redisProtectorSetting.Key);

        services.TryAddSingleton<IProtectorService, ProtectorManager>();

        return services;
    }

    /// <summary>
    /// Configures a CORS policy for an ASP.NET Core application using the IServiceCollection interface. 
    /// This allows cross-origin requests from any origin, with any HTTP method and header.
    /// </summary>
    /// <param name="services">The IServiceCollection interface used to register services with the application's dependency injection container.</param>
    /// <returns>The IServiceCollection instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <see cref="IServiceProvider"/> instance obtained from the specified <see cref="IServiceCollection"/> is null.</exception>
    public static IServiceCollection AddCustomCors(this IServiceCollection services, Action<CorsSetting> configure)
    {
        CorsSetting corsSetting = new();
        configure?.Invoke(corsSetting);

        services.AddCors(options =>
        {
            options.AddPolicy(Assembly.GetCallingAssembly().GetName().Name, builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins(corsSetting.Origins));
        });

        return services;
    }
}