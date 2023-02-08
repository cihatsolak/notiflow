﻿namespace Notiflow.Lib.Documentation.Filters
{
    internal sealed class TenantTokenOperationFilter : SwaggerOperationFilter
    {
        public override void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            base.Apply(operation, context);

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-tenant-token",
                In = ParameterLocation.Header,
                Required = false,
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
}
