namespace Puzzle.Lib.File;

/// <summary>
/// Extension methods for registering FTP settings to the service collection.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddFtpService(this IServiceCollection services, Action<FtpSetting> configure)
    {
        FtpSetting ftpSetting = new();
        configure?.Invoke(ftpSetting);

        services.Configure(configure);

        services.TryAddScoped<IFtpService>(provider =>
        {
            AsyncFtpClient asyncFtpClient = new(
                ftpSetting.Ip,
                ftpSetting.Username,
                ftpSetting.Password,
                ftpSetting.Port);

            asyncFtpClient.AutoConnect().Wait();
            asyncFtpClient.Config.RetryAttempts = 2;

            if (!asyncFtpClient.IsAuthenticated || !asyncFtpClient.IsConnected)
                throw new FtpException("Failed to connect via ftp. Check your credentials.");

            return new FtpManager(
                asyncFtpClient, 
                provider.GetRequiredService<IOptions<FtpSetting>>(),
                provider.GetRequiredService<ILogger<FtpManager>>());
        });

        return services;
    }

    /// <summary>
    /// Adds a default file provider that uses the physical file system to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to which the default file provider should be added.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDefaultFileProvider(this IServiceCollection services)
    {
        return services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
    }
}
