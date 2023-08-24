﻿namespace Notiflow.Backoffice.Persistence;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddPostgreSql<NotiflowDbContext>(nameof(NotiflowDbContext));

        services
            .AddScoped<ICustomerReadRepository, CustomerReadRepository>()
            .AddScoped<ICustomerWriteRepository, CustomerWriteRepository>()
            .AddScoped<IDeviceReadRepository, DeviceReadRepository>()
            .AddScoped<IDeviceWriteRepository, DeviceWriteRepository>()
            .AddScoped<ITextMessageHistoryReadRepository, TextMessageHistoryReadRepository>()
            .AddScoped<ITextMessageHistoryWriteRepository, TextMessageHistoryWriteRepository>();

        services.AddScoped<INotiflowUnitOfWork, NotiflowUnitOfWork>();
        
        services.SeedAsync().Wait();

        return services;
    }
}
