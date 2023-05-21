IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
        .AddNotiflowDbSetting()
        .AddMassTransit();

        services.AddHostedService<NotificationServiceWorker>();
    })
    .Build();

await host.RunAsync();
