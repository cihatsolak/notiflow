namespace Notiflow.IdentityServer;

internal static class ServiceCollectionContainerBuilderExtensions
{
    internal static IServiceCollection AddWebDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        JwtTokenSetting jwtTokenSetting = configuration.GetRequiredSection(nameof(JwtTokenSetting)).Get<JwtTokenSetting>();

        services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
        });

        services.AddJwtAuthentication(options =>
        {
            options.Audiences = jwtTokenSetting.Audiences;
            options.Issuer = jwtTokenSetting.Issuer;
            options.AccessTokenExpirationMinute = jwtTokenSetting.AccessTokenExpirationMinute;
            options.RefreshTokenExpirationMinute = jwtTokenSetting.RefreshTokenExpirationMinute;
            options.SecurityKey = jwtTokenSetting.SecurityKey;
        });

        services.AddRouteSettings();
        services.AddSwagger();
        services.AddGzipResponseFastestCompress();
        services.AddHttpSecurityPrecautions();
        services.AddApiVersion();
        services.AddConfigureHealthChecks();

        return services;
    }
}
