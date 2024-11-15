using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace LearnLogic.Infra.CrossCutting.APIConfiguration.Swagger
{
    public class RemoveODataSchemasFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Filtra os schemas que têm "OData" ou "Edm" no nome
            var schemasToRemove = swaggerDoc.Components.Schemas
                .Where(schema => schema.Key.Contains("OData") || schema.Key.Contains("Edm"))
                .ToList();

            // Remove esses schemas do documento
            foreach (var schema in schemasToRemove)
            {
                swaggerDoc.Components.Schemas.Remove(schema.Key);
            }
        }
    }
}
