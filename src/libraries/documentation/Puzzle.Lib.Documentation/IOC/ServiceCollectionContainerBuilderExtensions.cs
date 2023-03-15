namespace Puzzle.Lib.Documentation.IOC
{
    /// <summary>
    /// Extension methods for configuring Swagger in an IServiceCollection.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
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
            IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(SwaggerSetting));
            services.Configure<SwaggerSetting>(configurationSection);
            SwaggerSetting swaggerSetting = configurationSection.Get<SwaggerSetting>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.SwaggerDoc(swaggerSetting.Version,
                    new()
                    {
                        Title = swaggerSetting.Title,
                        Description = swaggerSetting.Description,
                        Version = swaggerSetting.Version,
                        Contact = new()
                        {
                            Email = swaggerSetting.ContactEmail,
                            Name = swaggerSetting.ContactName,
                            Url = new(swaggerSetting.ContactUrl)
                        },
                        License = new()
                        {
                            Name = swaggerSetting.LicenseName,
                            Url = new(swaggerSetting.LicenseUrl)
                        },
                        Extensions = new Dictionary<string, IOpenApiExtension>
                        {
                          { "x-logo", new OpenApiObject
                            {
                               {"url", new OpenApiString(swaggerSetting.LogoUrl)}
                            }
                          }
                        }
                    });

                AddIncludeXmlComments(options);
                AddOperationFilters(options, swaggerSetting);
                AddJwtSecurityScheme(options, swaggerSetting);
                AddBasicSecurityScheme(options, swaggerSetting);
            });

            return services;
        }

        /// <summary>
        /// Adds operation filters to Swagger, if the 'IsHaveDefaultHeaders' property of the SwaggerSetting is true.
        /// </summary>
        /// <param name="options">The SwaggerGenOptions instance to configure.</param>
        /// <param name="swaggerSetting">swagger settings obtained from configuration file</param>
        private static void AddOperationFilters(SwaggerGenOptions options, SwaggerSetting swaggerSetting)
        {
            if (!swaggerSetting.IsHaveDefaultHeaders)
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
        /// <param name="swaggerSetting">swagger settings obtained from configuration file</param>
        private static void AddJwtSecurityScheme(SwaggerGenOptions options, SwaggerSetting swaggerSetting)
        {
            if (!swaggerSetting.IsHaveJwtSecurityScheme)
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
        /// <param name="swaggerSetting">swagger settings obtained from configuration file</param>
        private static void AddBasicSecurityScheme(SwaggerGenOptions options, SwaggerSetting swaggerSetting)
        {
            if (!swaggerSetting.IsHaveBasicSecurityScheme)
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