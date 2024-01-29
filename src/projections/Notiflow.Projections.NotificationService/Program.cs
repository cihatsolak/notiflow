using Puzzle.Lib.Host;

IHost host = Host.CreateDefaultBuilder(args)
    .AddAppConfiguration()
    .AddServiceValidateScope()
    .AddShutdownTimeOut()
    .ConfigureServices((builder, services) =>
    {
        services.AddCustomMassTransit(builder.Configuration);
        services.AddScoped<IDbConnection>(provider => new NpgsqlConnection(builder.Configuration.GetConnectionString("BackOfficeDb")));

        services.AddHostedService<NotificationServiceWorker>();
    })
    .UseSerilog(Puzzle.Lib.Logging.HostBuilderExtensions.ConfigureLogging)
    .Build();

var logger = host.Services.GetRequiredService<ILogger<NotificationServiceWorker>>();

try
{
    logger.LogInformation("Starting worker service.");

    await host.RunAsync();
}
catch (Exception ex)
{
    logger.LogCritical(ex, "Host terminated unexpectedly.");
    await host.StopAsync();
}
finally
{
    host.Dispose();
}
