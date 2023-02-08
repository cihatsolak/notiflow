namespace Notiflow.Lib.Documentation.Filters
{
    internal sealed class CorrelationIdOperationFilter : SwaggerOperationFilter
    {
        public override void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            base.Apply(operation, context);

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-correlation-id",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Description = "unique identity communicated between microservices",
                    MaxLength = 36,
                    Title = "x-correlation-id"
                }
            });
        }
    }
}
