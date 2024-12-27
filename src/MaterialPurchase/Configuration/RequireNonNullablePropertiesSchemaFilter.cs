using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MaterialPurchase.Configuration;

public class RequireNonNullablePropertiesSchemaFilter : ISchemaFilter
{
    /// <summary>
    /// Add to model.Required all properties where Nullable is false.
    /// </summary>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var additionalRequiredProps = schema.Properties
            .Where(x => !x.Value.Nullable && !schema.Required.Contains(x.Key))
            .Select(x => x.Key);
        foreach (var propKey in additionalRequiredProps)
        {
            schema.Required.Add(propKey);
        }
    }
}