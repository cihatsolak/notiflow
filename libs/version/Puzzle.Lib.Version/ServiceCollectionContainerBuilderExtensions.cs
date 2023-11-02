namespace Puzzle.Lib.Version;

/// <summary>
/// Provides extension methods for registering API versioning services in the dependency injection container.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    private const string API_VERSION_HEADER_NAME = "x-api-version";
    private const string API_VERSION_QUERY_STRING_PARAMETER = "api-version";

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

        IApiVersioningBuilder apiVersioningBuilder = services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new(apiVersionSetting.MajorVersion, apiVersionSetting.MinorVersion);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new HeaderApiVersionReader(API_VERSION_HEADER_NAME),
                new QueryStringApiVersionReader(API_VERSION_QUERY_STRING_PARAMETER),
                new UrlSegmentApiVersionReader()
                );
        });

        if (apiVersionSetting.EnableVersionedApiExplorer)
        {
            apiVersioningBuilder.AddApiExplorer(options =>
            {
                // Add the versioned API explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });
        }

        return services;
    }
}
