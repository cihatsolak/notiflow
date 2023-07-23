namespace Notiflow.IdentityServer.Middlewares;

internal static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Those who control the application lifecycle
    /// </summary>
    /// <remarks>OnStarted - OnStopping - OnStopped</remarks>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
    internal static IApplicationBuilder UseApplicationLifetimes(this IApplicationBuilder app)
    {
        return app.CacheTenantInformation();
    }
}
