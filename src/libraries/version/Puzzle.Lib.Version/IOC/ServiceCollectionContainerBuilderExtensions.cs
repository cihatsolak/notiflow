namespace Puzzle.Lib.Version.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Add api versioning
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <seealso cref="https://github.com/dotnet/aspnet-api-versioning"/>
        /// <returns>type of built-in service collection interface</returns>
        /// <exception cref="ArgumentNullException">thrown when the service provider cannot be built</exception>
        public static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            ApiVersionSetting apiVersionSetting = default;
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<ApiVersionSetting>(configuration.GetRequiredSection(nameof(ApiVersionSetting)));
            services.TryAddSingleton<IApiVersionSetting>(provider =>
            {
                apiVersionSetting = provider.GetRequiredService<IOptions<ApiVersionSetting>>().Value;
                return apiVersionSetting;
            });

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
        /// Add api versioning with provider
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <param name="errorResponseProvider">default error response provider</param>
        /// <seealso cref="https://github.com/dotnet/aspnet-api-versioning"/>
        /// <returns>type of built-in service collection interface</returns>
        /// <exception cref="ArgumentNullException">thrown when the service provider cannot be built</exception>
        public static IServiceCollection AddApiVersioningWithProvider(this IServiceCollection services, IErrorResponseProvider errorResponseProvider)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            ApiVersionSetting apiVersionSetting = default;
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<ApiVersionSetting>(configuration.GetRequiredSection(nameof(ApiVersionSetting)));
            services.TryAddSingleton<IApiVersionSetting>(provider =>
            {
                apiVersionSetting = provider.GetRequiredService<IOptions<ApiVersionSetting>>().Value;
                return apiVersionSetting;
            });

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
}
