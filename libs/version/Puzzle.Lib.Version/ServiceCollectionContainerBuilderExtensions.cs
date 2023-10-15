namespace Puzzle.Lib.Version;

/// <summary>
/// Provides extension methods for registering API versioning services in the dependency injection container.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    private const string API_VERSION_HEADER_NAME = "x-api-version";

    /// <summary>
    /// Adds API versioning services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add API versioning services to.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the service provider is null.</exception>
    public static IServiceCollection AddApiVersion(this IServiceCollection services, Action<ApiVersionSetting> configure)
    {
        ApiVersionSetting apiVersionSetting = new();
        configure?.Invoke(apiVersionSetting);

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new(apiVersionSetting.MajorVersion, apiVersionSetting.MinorVersion);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader(API_VERSION_HEADER_NAME);
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
    public static IServiceCollection AddApiVersioningWithProvider(this IServiceCollection services, Action<ApiVersionSetting> configure, IErrorResponseProvider errorResponseProvider)
    {
        ApiVersionSetting apiVersionSetting = new();
        configure?.Invoke(apiVersionSetting);

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new(apiVersionSetting.MajorVersion, apiVersionSetting.MinorVersion);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader(API_VERSION_HEADER_NAME);
            options.ErrorResponses = errorResponseProvider;
        });

        return services;
    }
}
