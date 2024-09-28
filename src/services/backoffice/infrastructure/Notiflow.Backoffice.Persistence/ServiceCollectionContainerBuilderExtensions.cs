namespace Notiflow.Backoffice.Persistence;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ILogger logger= serviceProvider.GetRequiredService<ILogger<QueryViolationDbCommandInterceptor>>();

        SqlSetting sqlSetting = configuration.GetRequiredSection(nameof(NotiflowDbContext)).Get<SqlSetting>();

        services.AddPostgreSql<NotiflowDbContext>(options =>
        {
            options.IsSplitQuery = sqlSetting.IsSplitQuery;
            options.ConnectionString = sqlSetting.ConnectionString;
            options.CommandTimeoutSecond = sqlSetting.CommandTimeoutSecond;
            options.Interceptors = [
                new HistoricalDbContextInterceptor(),
                new QueryViolationDbCommandInterceptor(logger)
            ];
        });

        services
            .AddScoped<ICustomerReadRepository, CustomerReadRepository>()
            .AddScoped<ICustomerWriteRepository, CustomerWriteRepository>()
            .AddScoped<IDeviceReadRepository, DeviceReadRepository>()
            .AddScoped<IDeviceWriteRepository, DeviceWriteRepository>()
            .AddScoped<ITextMessageHistoryReadRepository, TextMessageHistoryReadRepository>()
            .AddScoped<ITextMessageHistoryWriteRepository, TextMessageHistoryWriteRepository>()
            .AddScoped<INotificationHistoryReadRepository, NotificationHistoryReadRepository>()
            .AddScoped<INotificationHistoryWriteRepository, NotificationHistoryWriteRepository>()
            .AddScoped<IEmailHistoryReadRepository, EmailHistoryReadRepository>()
            .AddScoped<IEmailHistoryWriteRepository, EmailHistoryWriteRepository>();

        services.AddScoped<INotiflowUnitOfWork, NotiflowUnitOfWork>();

        services.SeedAsync(CancellationToken.None).Wait();

        return services;
    }
}
