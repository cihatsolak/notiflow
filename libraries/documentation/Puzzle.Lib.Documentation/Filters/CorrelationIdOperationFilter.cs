namespace Puzzle.Lib.Documentation.Filters
{
    internal sealed class CorrelationIdOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-correlation-id",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Description = "unique identity communicated between services",
                    MaxLength = 36,
                    Title = "x-correlation-id"
                }
            });
        }
    }
}
