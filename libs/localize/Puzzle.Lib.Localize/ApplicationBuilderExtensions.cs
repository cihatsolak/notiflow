namespace Puzzle.Lib.Localize;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
    {
        return app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseLocalizationWithEndpoint(this WebApplication app)
    {
        app.MapGet("api/languages/supported-cultures", (IOptions<RequestLocalizationOptions> localizationOptions) =>
        {
            var supportedCultures = localizationOptions.Value.SupportedCultures
                .Select(culture => new SupportedCulturesResponse
                {
                    Name = culture.Name,
                    DisplayName = culture.DisplayName
                });

            if (!supportedCultures.Any())
                return Results.NotFound();

            return Results.Ok(supportedCultures);
        }).WithOpenApi(operation => new(operation)
        {
            Summary = "Retrieves the list of supported cultures for languages.",
            Description = "The response containing the list of supported cultures."
        })
        .Produces(StatusCodes.Status200OK, typeof(SupportedCulturesResponse), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status401Unauthorized, typeof(SupportedCulturesResponse), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status404NotFound, typeof(SupportedCulturesResponse), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status500InternalServerError, typeof(SupportedCulturesResponse), MediaTypeNames.Application.Json)
        .WithTags("General");

        return app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
    }
}