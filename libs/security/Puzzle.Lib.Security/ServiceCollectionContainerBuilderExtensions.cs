using Puzzle.Lib.Security.Services.Encryptions;

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
    public static IServiceCollection AddProtocolService(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        services.Configure<HostingSetting>(configuration.GetRequiredSection(nameof(HostingSetting)));

        services.TryAddSingleton<IProtocolService, ProtocolManager>();

        return services;
    }

    /// <summary>
    /// Adds an encryption service to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the encryption service will be added.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddEncryptionService(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        services.Configure<EncryptionSetting>(configuration.GetRequiredSection(nameof(EncryptionSetting)));

        services.TryAddSingleton<IEncryptionService, EncryptionManager>();

        return services;
    }

    /// <summary>
    /// An IServiceCollection extension that enables the HTTP Strict Transport Security (HSTS) feature.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The IServiceCollection instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <see cref="IServiceProvider"/> instance obtained from the specified <see cref="IServiceCollection"/> is null.</exception>
    public static IServiceCollection AddHttpSecurityPrecautions(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        IWebHostEnvironment webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
        if (!webHostEnvironment.IsProduction())
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
    public static IServiceCollection AddProtectorServiceWithRedisStore(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        RedisProtectorSetting protectorSetting = configuration.GetRequiredSection(nameof(RedisProtectorSetting)).Get<RedisProtectorSetting>();

        services.AddDataProtection()
            .SetApplicationName(serviceProvider.GetRequiredService<IWebHostEnvironment>().ApplicationName)
            .SetDefaultKeyLifetime(TimeSpan.FromDays(protectorSetting.ExpirationDays))
            .PersistKeysToStackExchangeRedis(() => ConnectionMultiplexer.Connect(protectorSetting.ConnectionString).GetDatabase(protectorSetting.DatabaseNumber), protectorSetting.Key);

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
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        CorsSetting corsSetting = configuration.GetRequiredSection(nameof(CorsSetting)).Get<CorsSetting>();

        services.AddCors(options =>
        {
            options.AddPolicy(Assembly.GetEntryAssembly().GetName().Name, builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins(corsSetting.Origins));
        });

        return services;
    }
}