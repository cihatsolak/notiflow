IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
        .AddNotiflowDbSetting()
        .AddMassTransit();

        services.AddHostedService<TextMessageServiceWorker>();
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<TextMessageServiceWorker>>();
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
