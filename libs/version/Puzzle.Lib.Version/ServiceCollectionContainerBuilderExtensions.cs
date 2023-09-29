namespace Puzzle.Lib.Version;

/// <summary>
/// Provides extension methods for registering API versioning services in the dependency injection container.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// Adds API versioning services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add API versioning services to.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the service provider is null.</exception>
    public static IServiceCollection AddApiVersion(this IServiceCollection services, Action<ApiVersionSetting> setup)
    {
        ApiVersionSetting apiVersionSetting = new();
        setup?.Invoke(apiVersionSetting);

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new(apiVersionSetting.MajorVersion, apiVersionSetting.MinorVersion);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader(apiVersionSetting.HeaderName);
        });

        return services;
    }

    /// <summary>
    /// Adds API versioning services to the specified <see cref="IServiceCollection"/> using the specified <see cref="IErrorResponseProvider"/> for error responses.
    /// </summary>
    /// <param name="services">The service collection to add API versioning services to.</param>
    /// <param name="errorResponseProvider">The error response provider to use for error responses.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the service provider is null.</exception>
    public static IServiceCollection AddApiVersioningWithProvider(this IServiceCollection services, Action<ApiVersionSetting> setup, IErrorResponseProvider errorResponseProvider)
    {
        ApiVersionSetting apiVersionSetting = new();
        setup?.Invoke(apiVersionSetting);

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new(apiVersionSetting.MajorVersion, apiVersionSetting.MinorVersion);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader(apiVersionSetting.HeaderName);
            options.ErrorResponses = errorResponseProvider;
        });

        return services;
    }
}
