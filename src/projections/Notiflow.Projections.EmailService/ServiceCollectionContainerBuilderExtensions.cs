namespace Notiflow.Projections.EmailService;

internal static class ServiceCollectionContainerBuilderExtensions
{
    internal static IServiceCollection AddNotiflowDbSetting(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

        return services.Configure<NotiflowDbSetting>(configuration.GetRequiredSection(nameof(NotiflowDbSetting)));
    }
}
