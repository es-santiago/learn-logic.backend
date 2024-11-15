using LearnLogic.Infra.CrossCutting.APIConfiguration.Middleware;
using LearnLogic.Infra.CrossCutting.APIConfiguration.TokenMiddleware;
using Microsoft.AspNetCore.Builder;

namespace LearnLogic.Infra.CrossCutting.Extensions
{
    public static class ConfigureMiddleware
    {
        public static void MiddlewareConfig(this IApplicationBuilder app)
        {
            app.UseMiddleware<FriendlyExceptionResponseMiddleware>();
            app.UseMiddleware<TokenMiddleware>();
        }
    }
}
