﻿using System.Globalization;

namespace Notiflow.IdentityServer.Service;

public static class ServiceCollectionBuilderExtensions
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        RedisServerSetting redisServerSetting = configuration.GetRequiredSection(nameof(RedisServerSetting)).Get<RedisServerSetting>();
        FtpSetting ftpSetting = configuration.GetRequiredSection(nameof(FtpSetting)).Get<FtpSetting>();

        services.AddRedisService(options =>
        {
            options.ConnectionString = redisServerSetting.ConnectionString;
            options.AbortOnConnectFail = redisServerSetting.AbortOnConnectFail;
            options.AsyncTimeOutSecond = redisServerSetting.AsyncTimeOutSecond;
            options.ConnectTimeOutSecond = redisServerSetting.ConnectTimeOutSecond;
            options.Username = redisServerSetting.Username;
            options.Password = redisServerSetting.Password;
            options.DefaultDatabase = redisServerSetting.DefaultDatabase;
            options.AllowAdmin = redisServerSetting.AllowAdmin;
        });

        services.AddFtpService(options =>
        {
            options.Ip = ftpSetting.Ip;
            options.Port = ftpSetting.Port;
            options.Username = ftpSetting.Username;
            options.Password = ftpSetting.Password;
            options.Url = ftpSetting.Url;
            options.Domain = ftpSetting.Domain;
        });

        services.TryAddSingleton<ITokenService, TokenManager>();
        services.TryAddScoped<IAuthService, AuthManager>();
        services.TryAddScoped<ITenantService, TenantManager>();
        services.TryAddScoped<ITenantPermissionService, TenantPermissionManager>();
        services.TryAddScoped<IUserService, UserManager>();

        services
           .AddHttpContextAccessor()
           .AddServerSideValidation(configure =>
           {
               configure.CascadeMode = CascadeMode.Stop;
               configure.CultureInfo = CultureInfo.CurrentCulture;
           })
           .AddApiBehaviorWithValidationLogging(opt =>
           {
               opt.SuppressInferBindingSourcesForParameters = false;
               opt.SuppressInferBindingSourcesForParameters = false;
               opt.ErrorMessage = "Hata!";
               opt.IsProductionEnvironment = true; //TODO:
           });

        AddObservers(services);

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        return services;
    }

    private static void AddObservers(IServiceCollection services)
    {
        services.TryAddScoped<ITenantObserverSubject>(provider =>
        {
            TenantObserverSubject tenantObserverSubject = new();
            tenantObserverSubject.RegisterObserver(new TenantObserverTransferTenantInfoToCache(provider));

            return tenantObserverSubject;
        });
    }
}
