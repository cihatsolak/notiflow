namespace Puzzle.Lib.Documentation.Filters
{
    internal abstract class SwaggerOperationFilter : IOperationFilter
    {
        public virtual void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();
        }
    }
}
