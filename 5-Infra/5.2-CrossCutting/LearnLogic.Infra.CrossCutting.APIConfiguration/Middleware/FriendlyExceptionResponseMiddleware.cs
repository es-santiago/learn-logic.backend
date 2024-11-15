using LearnLogic.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LearnLogic.Infra.CrossCutting.APIConfiguration.Middleware
{
    public class FriendlyExceptionResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public FriendlyExceptionResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorMessage = exception.Message;

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var result = JsonConvert.SerializeObject(new { error = errorMessage });

            return context.Response.WriteAsync(result);
        }
    }
}
