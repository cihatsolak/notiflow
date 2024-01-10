namespace Notiflow.Backoffice.API;

internal static class HostBuilderExtensions
{
    internal static void Configured(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        hostBuilder
                .AddAppConfiguration()
                .AddServiceValidateScope()
                .AddShutdownTimeOut();

        SeriLogElasticSetting seriLogElasticSetting = configuration.GetRequiredSection(nameof(SeriLogElasticSetting)).Get<SeriLogElasticSetting>();

        hostBuilder.AddSeriLogWithElasticSearch(options =>
        {
            options.Address = seriLogElasticSetting.Address;
            options.Username = seriLogElasticSetting.Username;
            options.Password = seriLogElasticSetting.Password;
            options.IsRequiredAuthentication = seriLogElasticSetting.IsRequiredAuthentication;
        });
    }
}
