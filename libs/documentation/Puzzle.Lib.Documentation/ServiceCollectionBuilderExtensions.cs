namespace Puzzle.Lib.Documentation;

/// <summary>
/// Extension methods for configuring Swagger in an IServiceCollection.
/// </summary>
public static class ServiceCollectionBuilderExtensions
{
    /// <summary>
    /// Adds Swagger to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add Swagger to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services, Action<SwaggerSetting> configure)
    {
        SwaggerSetting swaggerSetting = new();
        configure?.Invoke(swaggerSetting);

        if (swaggerSetting.IsProductionEnvironment) return services;

        services.Configure(configure);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.ExampleFilters();
            options.DescribeAllParametersInCamelCase();
#pragma warning disable S1075 // URIs should not be hardcoded
            options.SwaggerDoc(swaggerSetting.Version, new OpenApiInfo
            {
                Title = swaggerSetting.Title,
                Version = swaggerSetting.Version,
                Description = swaggerSetting.Description,
                Contact = new OpenApiContact
                {
                    Name = swaggerSetting.ContactName,
                    Email = swaggerSetting.ContactEmail
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                },
                Extensions = new Dictionary<string, IOpenApiExtension>
                {
                  { "x-logo", new OpenApiObject
                    {
                      { "url", new OpenApiString("https://avatars.githubusercontent.com/u/9141961")}
                    }
                  }
                }
            });
#pragma warning restore S1075 // URIs should not be hardcoded

            AddIncludeXmlComments(options);
            AddOperationFilters(options);
            AddJwtSecurityScheme(options);
            AddBasicSecurityScheme(options);
        });

        services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

        return services;
    }

    /// <summary>
    /// Adds operation filters to Swagger
    /// </summary>
    /// <param name="options">The SwaggerGenOptions instance to configure.</param>
    private static void AddOperationFilters(SwaggerGenOptions options)
    {
        options.OperationFilter<TenantTokenOperationFilter>();
        options.OperationFilter<CorrelationIdOperationFilter>();
        options.OperationFilter<AcceptLanguageOperationFilter>();
    }

    /// <summary>
    /// Adds XML comments to Swagger, to provide descriptions for controllers, actions, and parameters.
    /// </summary>
    /// <param name="options">The SwaggerGenOptions instance to configure.</param>
    private static void AddIncludeXmlComments(SwaggerGenOptions options)
    {
        string xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
        string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }

    /// <summary>
    /// Adds a JWT security scheme to Swagger
    /// </summary>
    /// <param name="options">The SwaggerGenOptions instance to configure.</param>
    private static void AddJwtSecurityScheme(SwaggerGenOptions options)
    {
        OpenApiSecurityScheme jwtSecurityScheme = new()
        {
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization,
            Type = SecuritySchemeType.Http,
            Description = "Industry standard RFC 7519 method for representing claims securely between two parties.",
            BearerFormat = "JWT",
            Scheme = "bearer",
            Reference = new()
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        };

        options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
        options.AddSecurityRequirement(new()
        {
            { jwtSecurityScheme, Array.Empty<string>() }
        });
    }

    /// <summary>
    /// Adds a basic security scheme to Swagger, if the 'IsHaveBasicSecurityScheme' property of the SwaggerSetting is true.
    /// </summary>
    /// <param name="options">The SwaggerGenOptions instance to configure.</param>
    private static void AddBasicSecurityScheme(SwaggerGenOptions options)
    {
        OpenApiSecurityScheme basicSecurityScheme = new()
        {
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "Authenticate user with username and password.",
            Scheme = "basic",
            Reference = new()
            {
                Id = "BasicAuth",
                Type = ReferenceType.SecurityScheme
            }
        };

        options.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
        options.AddSecurityRequirement(new()
        {
            { basicSecurityScheme, Array.Empty<string>() },
        });
    }
}