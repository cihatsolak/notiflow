namespace Puzzle.Lib.Documentation.IOC
{
    /// <summary>
    /// Extension methods for configuring Swagger in an IServiceCollection.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Gets or sets the Swagger settings.
        /// </summary>
        private static SwaggerSetting SwaggerSetting { get; set; }

        /// <summary>
        /// Adds Swagger to the service collection.
        /// </summary>
        /// <param name="services">The service collection to add Swagger to.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<SwaggerSetting>(configuration.GetRequiredSection(nameof(SwaggerSetting)));

            //Todo: settings class will be checked (SwaggerSetting)

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.SwaggerDoc(SwaggerSetting.Version,
                    new()
                    {
                        Title = SwaggerSetting.Title,
                        Description = SwaggerSetting.Description,
                        Version = SwaggerSetting.Version,
                        Contact = new()
                        {
                            Email = SwaggerSetting.ContactEmail,
                            Name = SwaggerSetting.ContactName,
                            Url = new(SwaggerSetting.ContactUrl)
                        },
                        License = new()
                        {
                            Name = SwaggerSetting.LicenseName,
                            Url = new(SwaggerSetting.LicenseUrl)
                        },
                        Extensions = new Dictionary<string, IOpenApiExtension>
                        {
                          { "x-logo", new OpenApiObject
                            {
                               {"url", new OpenApiString(SwaggerSetting.LogoUrl)}
                            }
                          }
                        }
                    });

                AddOperationFilters(options);
                AddIncludeXmlComments(options);
                AddJwtSecurityScheme(options);
                AddBasicSecurityScheme(options);
            });

            return services;
        }

        /// <summary>
        /// Adds operation filters to Swagger, if the 'IsHaveDefaultHeaders' property of the SwaggerSetting is true.
        /// </summary>
        /// <param name="options">The SwaggerGenOptions instance to configure.</param>
        private static void AddOperationFilters(SwaggerGenOptions options)
        {
            if (!SwaggerSetting.IsHaveDefaultHeaders)
                return;

            options.OperationFilter<TenantTokenOperationFilter>();
            options.OperationFilter<CorrelationIdOperationFilter>();
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
        /// Adds a JWT security scheme to Swagger, if the 'IsHaveJwtSecurityScheme' property of the SwaggerSetting is true.
        /// </summary>
        /// <param name="options">The SwaggerGenOptions instance to configure.</param>
        private static void AddJwtSecurityScheme(SwaggerGenOptions options)
        {
            if (!SwaggerSetting.IsHaveJwtSecurityScheme)
                return;

            OpenApiSecurityScheme jwtSecurityScheme = new()
            {
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization,
                Type = SecuritySchemeType.Http,
                Description = "user authentication and authorization",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Reference = new()
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtSecurityScheme);
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
            if (!SwaggerSetting.IsHaveBasicSecurityScheme)
                return;

            OpenApiSecurityScheme basicSecurityScheme = new()
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "user authentication and authorization",
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
}