using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;

namespace LearnLogic.Infra.CrossCutting.APIConfiguration.Swagger
{
    public static class ConfigureSwagger
    {
        public static void AddSwaggerServices(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(ConfigureSwaggerGen);
        }

        private static void ConfigureSwaggerGen(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Learn Logic",
                Description = "Development of a logic learning platform for the TCC project.",
                Contact = new OpenApiContact { Name = "Lucas Santiago", Email = "lcscostasantiago@gmail.com", Url = new Uri("https://www.linkedin.com/in/lucascostasantiago/") }
            });

            options.DocumentFilter<RemoveODataRoutesDocumentFilter>();

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Input the JWT like: Bearer {your token}",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(ConfigureSwaggerUI);
        }

        private static void ConfigureSwaggerUI(SwaggerUIOptions options)
        {
            options.DefaultModelExpandDepth(2);
            options.DefaultModelsExpandDepth(-1);
            options.DisplayOperationId();
            options.DisplayRequestDuration();
            options.EnableDeepLinking();
            options.EnableFilter();
            options.ShowExtensions();
            options.EnableValidator();
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        }
    }
}
