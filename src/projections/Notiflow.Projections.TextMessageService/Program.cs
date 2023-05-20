IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit();
        services.AddHostedService<TextMessageServiceWorker>();
    })
    .Build();

host.Run();
