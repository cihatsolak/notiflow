namespace Notiflow.Lib.Host.Builders
{
    public static class HostExtension
    {
        /// <summary>
        /// Add application configuration
        /// </summary>
        /// <param name="hostBuilder">type of built-in host builder interface</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.ihostbuilder?view=dotnet-plat-ext-6.0"/>
        /// <returns>type of built-in host builder interface</returns>
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
        /// Add service validate scope
        /// </summary>
        /// <param name="hostBuilder">type of built-in host builder interface</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.serviceprovideroptions.validatescopes?view=dotnet-plat-ext-6.0"/>
        /// <returns>type of built-in host builder interface</returns>
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
        /// Add shut down time out
        /// </summary>
        /// <param name="hostBuilder">type of built-in host builder interface</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.hostoptions.shutdowntimeout?view=dotnet-plat-ext-6.0"/>
        /// <returns>type of built-in host builder interface</returns>
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
