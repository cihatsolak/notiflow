namespace Notiflow.Panel;

internal static class LocalizationBuilderExtensions
{
    private const string DEFAULT_RESOURCE_PATH = "Resources";

    internal static IServiceCollection AddMultiLanguage(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = DEFAULT_RESOURCE_PATH);

        services.AddMvc()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, options =>
        {
            options.ResourcesPath = DEFAULT_RESOURCE_PATH;
        })
        .AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, localizerFactory) =>
            {
                return localizerFactory.Create(typeof(AuthManager));
            };
        });

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("tr-TR"),
                new CultureInfo("en-US"),
                new CultureInfo("fr-FR"),
            };

            options.DefaultRequestCulture = new RequestCulture("tr-TR");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.FallBackToParentUICultures = true;
        });


        return services;
    }
}
