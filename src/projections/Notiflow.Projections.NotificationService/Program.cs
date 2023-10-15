IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddCustomMassTransit(builder.Configuration);

        services.AddScoped<IDbConnection>(provider => new NpgsqlConnection(builder.Configuration.GetConnectionString("BackOfficeDb")));

        services.AddHostedService<NotificationServiceWorker>();
    })
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
