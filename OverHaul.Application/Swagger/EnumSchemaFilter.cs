using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Swagger;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var items = new Dictionary<int, string>();
            foreach (var i in Enum.GetValues(context.Type))
            {
                var key = Convert.ToInt32(i);
                if (!items.ContainsKey(key))
                    items.Add(key: key, value: Enum.GetName(enumType: context.Type, value: i));
            }
            ;
            schema.Description += string.Join(
                separator: ", ",
                values: items.Select(x => x.Key + ": " + x.Value)
            );
        }
    }
}
