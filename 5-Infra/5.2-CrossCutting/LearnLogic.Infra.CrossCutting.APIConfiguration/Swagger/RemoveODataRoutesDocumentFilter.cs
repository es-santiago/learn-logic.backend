using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace LearnLogic.Infra.CrossCutting.APIConfiguration.Swagger
{
    public class RemoveODataRoutesDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Filtra as rotas que contêm "$metadata" ou que são exatamente "v1"
            var pathsToRemove = swaggerDoc.Paths
                .Where(pathItem => pathItem.Key.Contains("$metadata") ||
                                   pathItem.Key.Equals("/v1", System.StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var path in pathsToRemove)
            {
                swaggerDoc.Paths.Remove(path.Key); // Remove essas rotas do documento Swagger
            }
        }
    }
}
