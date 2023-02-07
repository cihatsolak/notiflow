namespace Notiflow.Lib.Documentation.Infrastructure.OperationFilters
{
    internal abstract class SwaggerOperationFilter : IOperationFilter
    {
        public virtual void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();
        }
    }
}
