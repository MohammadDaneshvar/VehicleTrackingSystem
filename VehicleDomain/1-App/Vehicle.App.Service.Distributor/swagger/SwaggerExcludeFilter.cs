using Framework.Application.Common.Extentions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vehicle.AppService.Contracts.Attributes;

namespace Framework.Application.Common.Attributes
{
    public class SwaggerIgnoreFilter : Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter
    {
        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            if (schema.properties == null || type == null)
                return;

            var excludedProperties = type.GetProperties()
                                         .Where(t =>
                                                t.GetCustomAttribute<SwaggerIgnoreAttribute>()
                                                != null);

            foreach (var excludedProperty in excludedProperties)
            {
                if (schema.properties.ContainsKey(excludedProperty.Name))
                    schema.properties.Remove(excludedProperty.Name);
            }
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {

        }
    }
    public class SwaggerIgnoreParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {


        }
    }

    public class TagDescriptionsDocumentFilter : Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (SwaggerTag item in Enum.GetValues(typeof(SwaggerTag)))
            {
                swaggerDoc.Tags.Add(
                    new OpenApiTag { Name = item.ToString(), Description = item.GetDescription() });
            }
        }
    }

    public class SwaggerIgnoreIOperationFilterFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Tags.Clear();
            Type parameterType = context.MethodInfo.GetParameters().FirstOrDefault()?.ParameterType;
            var swaggerAtt = parameterType?.GetCustomAttribute<Swagger>();
            if (swaggerAtt != null && swaggerAtt.Tags != null)
                swaggerAtt.Tags.ToList().ForEach(x => operation.Tags
                .Add(new OpenApiTag
                {
                    Name = x.ToString(),
                    Description = x.GetDescription()
                })) ;

            var result = parameterType?.GetProperty("Result");
            if (result != null)
            {
                operation.Responses.Remove("200");
                operation.Responses.Add("200",

                new OpenApiResponse
                {
                    Description = "successful operation",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {

                        ["application/json"] = new OpenApiMediaType
                        {

                            Schema = context.SchemaGenerator.GenerateSchema(result.PropertyType, context.SchemaRepository)
                        }
                    }
                });
            }
            var ignoreProperties = parameterType?.GetProperties()
                .Where(x => x.GetCustomAttribute<SwaggerIgnoreAttribute>() != null).ToList();

            foreach (var item in operation.Parameters.ToList())
            {
                if (ignoreProperties.Any(x => x.Name == item.Name) || ignoreProperties.Any(x => x.Name == item.Name.Split('.').FirstOrDefault()))
                    operation.Parameters.Remove(item);
            }
        }
    }
}
