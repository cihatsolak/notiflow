namespace Notiflow.Lib.Documentation.Infrastructure.OperationFilters
{
    internal sealed class CorrelationIdOperationFilter : SwaggerOperationFilter
    {
        public override void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            base.Apply(operation, context);

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Correlation-Id",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Description = "unique identity communicated between microservices",
                    MaxLength = 36,
                    Title = "X-Correlation-Id"
                }
            });
        }
    }
}
