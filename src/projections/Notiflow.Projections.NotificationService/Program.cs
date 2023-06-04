IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
        .AddNotiflowDbSetting()
        .AddCustomMassTransit();

        services.AddHostedService<NotificationServiceWorker>();
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<NotificationServiceWorker>>();
var hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();

string applicationName = hostEnvironment.ApplicationName;

try
{
    logger.LogInformation("-- Starting web host: {@applicationName} --", applicationName);

    await host.RunAsync();
}
catch (Exception ex)
{
    logger.LogCritical(ex, "-- Host terminated unexpectedly. {@applicationName} -- ", applicationName);
    await host.StopAsync();
}
finally
{
    host.Dispose();
}
