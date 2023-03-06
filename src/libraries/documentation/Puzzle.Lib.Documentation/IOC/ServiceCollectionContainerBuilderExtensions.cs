namespace Puzzle.Lib.Documentation.IOC
{
    /// <summary>
    /// Extension methods for setting up MVC services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Is the class that has all the settings of swagger
        /// </summary>
        private static SwaggerSetting SwaggerSetting { get; set; }

        /// <summary>
        /// Add swagger documentation
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <returns>type of built-in service collection interface</returns>
        /// <see cref="https://swagger.io/"/>
        /// <seealso cref="AddOperationFilters(SwaggerGenOptions)"/>
        /// <seealso cref="AddIncludeXmlComments(SwaggerGenOptions)"/>
        /// <seealso cref="AddJwtSecurityScheme(SwaggerGenOptions)"/>
        /// <seealso cref="AddBasicSecurityScheme(SwaggerGenOptions)"/>
        /// <exception cref="ArgumentNullException">thrown when the service provider cannot be built</exception>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<SwaggerSetting>(configuration.GetRequiredSection(nameof(SwaggerSetting)));
            services.TryAddSingleton<ISwaggerSetting>(provider =>
            {
                SwaggerSetting = provider.GetRequiredService<IOptions<SwaggerSetting>>().Value;
                return SwaggerSetting;
            });

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

        private static void AddOperationFilters(SwaggerGenOptions options)
        {
            if (!SwaggerSetting.IsHaveDefaultHeaders)
                return;

            options.OperationFilter<TenantTokenOperationFilter>();
            options.OperationFilter<CorrelationIdOperationFilter>();
        }

        private static void AddIncludeXmlComments(SwaggerGenOptions options)
        {
            string xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }

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