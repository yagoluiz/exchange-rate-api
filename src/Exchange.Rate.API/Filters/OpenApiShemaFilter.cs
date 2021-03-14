using System;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Exchange.Rate.API.Filters
{
    public class OpenApiShemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Type = "string";
                schema.Enum.Clear();

                Enum.GetNames(context.Type)
                    .ToList()
                    .ForEach(enumValue => schema.Enum.Add(new OpenApiString(enumValue)));
            }
        }
    }
}
