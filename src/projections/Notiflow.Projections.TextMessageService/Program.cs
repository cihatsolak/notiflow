IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
        .AddNotiflowDbSetting()
        .AddMassTransit();

        services.AddHostedService<TextMessageServiceWorker>();
    })
    .Build();

host.Run();
