namespace Notiflow.Lib.Documentation.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Add swagger documentation
        /// </summary>
        /// <param name="builder">type of web application builder</param>
        /// <seealso cref="https://swagger.io/"/>
        /// <returns>type of web application builder</returns>
        /// <exception cref="ArgumentNullException">thrown if the service provider is null</exception>
        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            SwaggerSetting swaggerSetting = builder.Configuration.GetRequiredSection(nameof(SwaggerSetting)).Get<SwaggerSetting>();
            builder.Services.TryAddSingleton<ISwaggerSetting>(provider =>
            {
                return swaggerSetting;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
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
                          {"x-logo", new OpenApiObject
                            {
                               {"url", new OpenApiString(swaggerSetting.LogoUrl)}
                            }
                          }
                        }
                    });

                AddIncludeXmlComments(options);

                if (swaggerSetting.IsJwtSecurityScheme)
                {
                    AddJwtSecurityScheme(options);
                }

                if (swaggerSetting.IsBasicSecurityScheme)
                {
                    AddBasicSecurityScheme(options);
                }
            });

            return builder;
        }

        private static void AddIncludeXmlComments(SwaggerGenOptions options)
        {
            string xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }

        private static void AddJwtSecurityScheme(SwaggerGenOptions options)
        {
            OpenApiSecurityScheme jwtSecurityScheme = new()
            {
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization,
                Type = SecuritySchemeType.Http,
                Description = "Bearer {token}",
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
            OpenApiSecurityScheme basicSecurityScheme = new()
            {
                Type = SecuritySchemeType.Http,
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