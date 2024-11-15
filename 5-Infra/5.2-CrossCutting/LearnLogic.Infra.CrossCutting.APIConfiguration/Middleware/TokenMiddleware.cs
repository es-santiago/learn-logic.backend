using LearnLogic.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLogic.Infra.CrossCutting.APIConfiguration.TokenMiddleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserClaimsAccessor userClaimsAccessor)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (context.Request.Method != "OPTIONS" && !string.IsNullOrEmpty(authorizationHeader))
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = authorizationHeader.Replace("Bearer ", "");
                    var jwtToken = tokenHandler.ReadJwtToken(token);
                    var claimsLookup = ExtractClaims(jwtToken);
                    userClaimsAccessor.LoadClaims(claimsLookup);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            await _next(context);
        }

        private ILookup<string, string> ExtractClaims(JwtSecurityToken jwt)
        {
            return jwt.Claims.ToLookup(claim => claim.Type, claim => claim.Value);
        }
    }
}
