namespace Puzzle.Lib.Documentation.Filters;

internal sealed class AcceptLanguageOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept-Language",
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Description = "sets the preferred language",
                MaxLength = 5,
                Title = "accept-language"
            }
        });
    }
}