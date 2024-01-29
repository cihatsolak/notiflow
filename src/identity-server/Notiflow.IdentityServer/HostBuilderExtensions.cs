namespace Notiflow.IdentityServer;

internal static class HostBuilderExtensions
{
    internal static void HostConfigured(this WebApplicationBuilder builder)
    {
        builder.Host
                .AddAppConfiguration()
                .AddServiceValidateScope()
                .AddShutdownTimeOut();

        SeriLogElasticSetting seriLogElasticSetting = builder.Configuration.GetRequiredSection(nameof(SeriLogElasticSetting)).Get<SeriLogElasticSetting>();

        builder.Host.AddSeriLogWithElasticSearch(options =>
        {
            options.Address = seriLogElasticSetting.Address;
            options.Username = seriLogElasticSetting.Username;
            options.Password = seriLogElasticSetting.Password;
            options.IsRequiredAuthentication = seriLogElasticSetting.IsRequiredAuthentication;
        });
    }
}
