using Microsoft.AspNetCore.Builder;

namespace LearnLogic.Infra.CrossCutting.APIConfiguration.Controllers
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureDefaultServerSettings(this IApplicationBuilder app)
        {
            app.UseCors(corsPolicy =>
            {
                corsPolicy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .WithExposedHeaders("*");

            });
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
        }

        public static void ConfigureDefaultAuthSettings(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
