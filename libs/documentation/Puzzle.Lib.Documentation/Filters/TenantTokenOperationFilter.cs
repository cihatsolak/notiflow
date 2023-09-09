namespace Puzzle.Lib.Documentation.Filters;

internal sealed class TenantTokenOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "x-tenant-token",
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Description = "credential for multi-tenant applications",
                MaxLength = 36,
                Title = "x-tenant-token"
            }
        });
    }
}
