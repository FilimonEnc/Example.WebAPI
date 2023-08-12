using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Example.WebAPI.Utils
{
    public class EnumTypesSchemaFilter : ISchemaFilter
    {
        private readonly XDocument _xmlComments;

        public EnumTypesSchemaFilter(string xmlPath)
        {
            if (File.Exists(xmlPath))
            {
                _xmlComments = XDocument.Load(xmlPath);
            }
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (_xmlComments == null) return;

            if (schema.Enum != null && schema.Enum.Count > 0 &&
                context.Type != null && context.Type.IsEnum)
            {
                if (schema.Description != null && schema.Description.Contains("<p>Members:</p>")) return;
                schema.Description += "<p>Members:</p><ul>";

                var fullTypeName = context.Type.FullName;

                foreach (var enumMemberName in schema.Enum.OfType<OpenApiString>().Select(v => v.Value))
                {
                    var fullEnumMemberName = $"F:{fullTypeName}.{enumMemberName}";

                    var enumMemberComments = _xmlComments.Descendants("member")
                        .FirstOrDefault(m => m.Attribute("name")!.Value.Equals(fullEnumMemberName, StringComparison.OrdinalIgnoreCase));
                    if (enumMemberComments == null) continue;

                    var summary = enumMemberComments.Descendants("summary").FirstOrDefault();
                    if (summary == null) continue;

                    schema.Description += $"<li><i>{enumMemberName}</i> - {summary.Value.Trim()}</li>";

                }

                schema.Description += "</ul>";
            }
        }
    }

    public class EnumTypesDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var path in swaggerDoc.Paths.Values)
            {
                foreach (var operation in path.Operations.Values)
                {
                    foreach (var parameter in operation.Parameters)
                    {
                        var schemaReferenceId = parameter.Schema.Reference?.Id;
                        if (string.IsNullOrEmpty(schemaReferenceId)) continue;

                        var schema = context.SchemaRepository.Schemas[schemaReferenceId];

                        if (schema.Enum == null || schema.Enum.Count == 0) continue;

                        parameter.Description += "<p>Variants:</p>";

                        int cutStart = schema.Description.IndexOf("<ul>");
                        int cutEnd = schema.Description.IndexOf("</ul>") + 5;

                        parameter.Description += schema.Description
                            .Substring(cutStart, cutEnd - cutStart);
                    }
                }
            }

        }
    }
}
