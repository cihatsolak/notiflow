﻿namespace Puzzle.Lib.Host.Builders
{
    /// <summary>
    /// This class provides a set of extension methods for adding additional functionality to .NET Core applications.
    /// </summary>
    public static class HostExtension
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
                if (hostBuilderContext.HostingEnvironment.IsProduction())
                {
                    serviceProviderOptions.ValidateScopes = false;
                }
                else
                {
                    serviceProviderOptions.ValidateScopes = true;
                }
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
            hostBuilder.ConfigureHostOptions(configureOptions =>
            {
                configureOptions.ShutdownTimeout = TimeSpan.FromMinutes(10);
            });

            return hostBuilder;
        }
    }
}