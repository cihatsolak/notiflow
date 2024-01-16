using System.Security.Authentication;

namespace Puzzle.Lib.Auth;

/// <summary>
/// Provides extension methods to add JWT authentication and claim services to the <see cref="IServiceCollection"/> container.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// Adds JWT authentication services to the <see cref="IServiceCollection"/> container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, Action<JwtTokenSetting> configure)
    {
        JwtTokenSetting jwtTokenSetting = new();
        configure.Invoke(jwtTokenSetting);

        services.Configure(configure);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions =>
        {
            configureOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtTokenSetting.Issuer,
                ValidAudience = jwtTokenSetting.Audiences.First(),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSetting.SecurityKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                //RequireExpirationTime = true
                ClockSkew = TimeSpan.FromMinutes(1),
                NameClaimType = ClaimTypes.Name,
                RoleClaimType = ClaimTypes.Role
            };

            configureOptions.IncludeErrorDetails = true;

            configureOptions.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    string name = context?.Principal?.Identity?.Name;
                    if (string.IsNullOrEmpty(name))
                    {
                        context?.Fail("Unauthorized. Please re-login");
                    }
                    
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    //context.Response.ContentType = MediaTypeNames.Application.Json;

                    //return context.Response.WriteAsync(HandleJwtEventError(context.Exception.Message));
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    if (string.IsNullOrEmpty(context.Error))
                        context.Error = "invalid_token";

                    if (string.IsNullOrEmpty(context.ErrorDescription))
                        context.ErrorDescription = "This request requires a valid JWT access token to be provided";

                    if (context.AuthenticateFailure is SecurityTokenExpiredException)
                    {
                        var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                        context.Response.Headers.Append("x-token-expired", authenticationException.Expires.ToString("o"));
                        context.ErrorDescription = $"The token expired on {authenticationException.Expires:o}";
                    }

                    return context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        error = context.Error,
                        error_description = context.ErrorDescription
                    }));
                },
                OnForbidden = context =>
                {
                    //context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    //context.Response.ContentType = MediaTypeNames.Application.Json;

                    //return context.Response.WriteAsync(HandleJwtEventError("You are not authorized to access this resource."));
                    return Task.CompletedTask;
                },
                OnMessageReceived = context =>
                {
                    string token = context.Request.Query["access_token"];
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        context.Token = token;
                    }

                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }

    /// <summary>
    /// Adds claim services to the <see cref="IServiceCollection"/> container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddClaimService(this IServiceCollection services)
    {
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.TryAddSingleton<IClaimService, ClaimManager>();

        return services;
    }
}