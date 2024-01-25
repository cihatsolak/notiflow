using System.Globalization;

namespace Puzzle.Lib.Host.Infrastructure;

/// <summary>
/// This class provides a set of extension methods for adding additional functionality to .NET Core applications.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Adds application configuration to the provided IHostBuilder instance by including JSON configuration files.
    /// </summary>
    /// <param name="hostBuilder">The IHostBuilder instance to which the configuration is added.</param>
    /// <returns>The modified IHostBuilder instance.</returns>
    public static IHostBuilder AddAppConfiguration(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
        {
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationBuilder.AddJsonFile($"appsettings.{hostBuilderContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
            configurationBuilder.AddEnvironmentVariables();
        });

        return hostBuilder;
    }

    /// <summary>
    /// Adds service scope validation to the provided IHostBuilder instance based on the hosting environment.
    /// </summary>
    /// <param name="hostBuilder">The IHostBuilder instance to which the validation is added.</param>
    /// <returns>The modified IHostBuilder instance.</returns>
    public static IHostBuilder AddServiceValidateScope(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseDefaultServiceProvider((hostBuilderContext, serviceProviderOptions) =>
        {
            serviceProviderOptions.ValidateScopes = !hostBuilderContext.HostingEnvironment.IsProduction();

        });

        return hostBuilder;
    }

    /// <summary>
    /// Adds a timeout duration for graceful shutdown to the provided IHostBuilder instance.
    /// </summary>
    /// <param name="hostBuilder">The IHostBuilder instance to which the timeout duration is added.</param>
    /// <returns>The modified IHostBuilder instance.</returns>
    public static IHostBuilder AddShutdownTimeOut(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureHostOptions(options =>
        {
            options.ShutdownTimeout = TimeSpan.FromMinutes(2);
        });

        return hostBuilder;
    }

    /// <summary>
    /// Asynchronously starts the specified <see cref="IHost"/> instance.
    /// </summary>
    /// <param name="host">The <see cref="IHost"/> to start.</param>
    /// <seealso cref="IHost"/>
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task StartProjectAsync(this WebApplication webApplication)
    {
        try
        {
            webApplication.Logger.LogInformation("-- Starting web host: {applicationName} --", webApplication.Environment.ApplicationName);
            await webApplication.RunAsync();
        }
        catch (Exception exception)
        {
            webApplication.Logger.LogCritical(exception, "-- Host terminated unexpectedly. {applicationName} -- ", webApplication.Environment.ApplicationName);
            await webApplication.StopAsync();
        }
        finally
        {
            await webApplication.DisposeAsync();
        }
    }
}
