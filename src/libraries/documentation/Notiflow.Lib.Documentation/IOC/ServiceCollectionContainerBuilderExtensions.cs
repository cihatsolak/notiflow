namespace Notiflow.Lib.Documentation.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        private static SwaggerSetting SwaggerSetting { get; set; }

        /// <summary>
        /// Add swagger documentation
        /// </summary>
        /// <param name="builder">type of web application builder</param>
        /// <returns>type of web application builder</returns>
        /// <see cref="https://swagger.io/"/>
        /// <seealso cref="AddIncludeXmlComments(SwaggerGenOptions)"/>
        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            SwaggerSetting = builder.Configuration.GetRequiredSection(nameof(SwaggerSetting)).Get<SwaggerSetting>();
            builder.Services.TryAddSingleton<ISwaggerSetting>(provider =>
            {
                return SwaggerSetting;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
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

            return builder;
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