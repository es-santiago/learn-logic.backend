using LearnLogic.Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.NewtonsoftJson;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Newtonsoft.Json.Converters;

namespace LearnLogic.Infra.CrossCutting.APIConfiguration.Controllers
{
    public static class ConfigureControllers
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.ConfigureJsonOptions();
            services.ConfigureOdata();
            services.ConfigureRouting();
            services.ConfigureCors();
            services.AddHttpContextAccessor();
        }

        private static void ConfigureJsonOptions(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyConverterExtension());
                });
        }

        private static void ConfigureOdata(this IServiceCollection services)
        {
            var edmModel = BuildEdmModel();

            services
                .AddControllers()
                .AddNewtonsoftJson(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                    jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
                    jsonOptions.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
                    jsonOptions.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
                    jsonOptions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            if (edmModel != null)
            {
                services
                    .AddControllers()
                    .AddODataNewtonsoftJson()
                    .AddOData(oDataOptions =>
                    {
                        oDataOptions.AddRouteComponents("v1", edmModel)
                                    .Count()
                                    .Filter()
                                    .OrderBy()
                                    .Expand()
                                    .Select()
                                    .SetMaxTop(1000);
                    });
            }
        }

        private static void ConfigureRouting(this IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        private static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("*");
                });
            });
        }

        private static IEdmModel BuildEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EnableLowerCamelCase();
            return modelBuilder.GetEdmModel();
        }
    }
}
