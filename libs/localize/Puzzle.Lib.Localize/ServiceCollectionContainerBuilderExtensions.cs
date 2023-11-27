namespace Puzzle.Lib.Localize;

public static class ServiceCollectionContainerBuilderExtensions
{
    private const string ENGLISH_CULTURE = "en-US";
    private const string TURKISH_CULTURE = "tr-TR";
    private const string FRANCE_CULTURE = "fr-FR";

    public static IServiceCollection AddWebApiLocalize(this IServiceCollection services)
    {
        services.AddLocalization();
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo(ENGLISH_CULTURE),
                new CultureInfo(TURKISH_CULTURE)
            };

            options.DefaultRequestCulture = new(TURKISH_CULTURE);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.ApplyCurrentCultureToResponseHeaders = true;
        });

        services.TryAddScoped(typeof(ILocalizerService<>), typeof(LocalizerManager<>));

        return services;
    }

    public static IServiceCollection AddWebUILocalize(this IServiceCollection services, Action<LocalizeSetting> configure)
    {
        LocalizeSetting localizeSetting = new();
        configure?.Invoke(localizeSetting);

        services.AddLocalization(options =>
        {
            options.ResourcesPath = localizeSetting.ResourcesPath;
        });

        services.AddMvc()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, options =>
        {
            options.ResourcesPath = localizeSetting.ResourcesPath;
        })
        .AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, localizerFactory) =>
            {
                return localizerFactory.Create(localizeSetting.SharedDataAnnotationBaseName, localizeSetting.SharedDataAnnotationLocation);
            };
        });

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo(TURKISH_CULTURE),
                new CultureInfo(ENGLISH_CULTURE),
                new CultureInfo(FRANCE_CULTURE),
            };

            options.DefaultRequestCulture = new RequestCulture(TURKISH_CULTURE);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.FallBackToParentUICultures = true;
        });

        services.TryAddScoped(typeof(ILocalizerService<>), typeof(LocalizerManager<>));

        return services;
    }
}