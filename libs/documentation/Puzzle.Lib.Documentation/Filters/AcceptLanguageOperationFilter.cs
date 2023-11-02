namespace Puzzle.Lib.Documentation.Filters;

internal sealed class AcceptLanguageOperationFilter : IOperationFilter
{
    private readonly RequestLocalizationOptions _requestLocalizationOptions;

    public AcceptLanguageOperationFilter(IServiceProvider serviceProvider)
    {
        _requestLocalizationOptions = serviceProvider.GetService<IOptions<RequestLocalizationOptions>>()?.Value;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        var supportedCultures = _requestLocalizationOptions?.SupportedCultures?
                                   .Select(culture => new OpenApiString(culture.TwoLetterISOLanguageName))
                                   .ToList<IOpenApiAny>();

        OpenApiSchema openApiSchema;

        if (supportedCultures is null || !supportedCultures.Any())
        {
            openApiSchema = new OpenApiSchema
            {
                Type = "string",
                Description = "sets the preferred language",
                MaxLength = 5,
                Title = "accept-language"
            };
        }
        else
        {
            openApiSchema = new OpenApiSchema
            {
                Type = "string",
                Enum = supportedCultures
            };
        }

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "accept-language",
            In = ParameterLocation.Header,
            Required = false,
            Schema = openApiSchema
        });
    }
}