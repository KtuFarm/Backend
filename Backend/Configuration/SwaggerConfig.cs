using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Backend.Configuration
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SwaggerConfig : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Api-Request",
                In = ParameterLocation.Header,
                Description = "Api Request header",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("true")
                }
            });
        }
    }
}
