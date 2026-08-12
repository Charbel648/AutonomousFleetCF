using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fleet.API.Swagger;

public class TenantHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var relativePath = context.ApiDescription.RelativePath ?? string.Empty;

        if (relativePath.StartsWith("auth", StringComparison.OrdinalIgnoreCase))
            return;

        operation.Parameters ??= new List<OpenApiParameter>();

        if (operation.Parameters.Any(parameter => parameter.Name == "X-Tenant-Id"))
            return;

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Tenant-Id",
            In = ParameterLocation.Header,
            Required = true,
            Description = "Active tenant id. Use tenant-a for Swagger testing.",
            Schema = new OpenApiSchema
            {
                Type = "string",
                Default = new OpenApiString("tenant-a")
            }
        });
    }
}
