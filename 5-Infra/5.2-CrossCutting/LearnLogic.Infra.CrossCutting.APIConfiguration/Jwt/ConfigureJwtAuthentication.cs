using LearnLogic.Domain.Exceptions;
using LearnLogic.Domain.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LearnLogic.Infra.CrossCutting.APIConfiguration.Jwt
{
    public static class ConfigureJwtAuthentication
    {
        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new CustomException("services");
            }

            if (configuration == null)
            {
                throw new CustomException("configuration");
            }

            IConfigurationSection section = configuration.GetSection("JwtSettings");
            services.Configure<AppJwtSettings>(section);
            AppJwtSettings appSettings = section.Get<AppJwtSettings>();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(delegate (AuthenticationOptions x)
            {
                x.DefaultAuthenticateScheme = "Bearer";
                x.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(delegate (JwtBearerOptions x)
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.Audience,
                    ValidIssuer = appSettings.Issuer
                };
            });
            return services;
        }
    }
}
