namespace Puzzle.Lib.Localize;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddLocalize(this IServiceCollection services)
    {
        services.AddLocalization();
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("tr-TR")
            };

            options.DefaultRequestCulture = new("tr-TR");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.ApplyCurrentCultureToResponseHeaders = true;
        });

        services.TryAddScoped(typeof(ILocalizerService<>), typeof(LocalizerManager<>));

        return services;
    }
}