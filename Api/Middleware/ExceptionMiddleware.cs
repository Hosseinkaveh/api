using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Middleware
{
    public class ExceptionMiddleware
    {
        // A function that can process an HTTP request.
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

    // Provides information about the hosting environment an application is running
        private readonly  IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;


                var response = _env.IsDevelopment()?
                new ApiExeption(context.Response.StatusCode,ex.Message,ex.StackTrace?.ToString()):
                new ApiExeption(context.Response.StatusCode,"Internal Server Error");

                var option = new JsonSerializerOptions
                {     
                    PropertyNamingPolicy= JsonNamingPolicy.CamelCase
                };
                
                var json = JsonSerializer.Serialize(response,option);

                await context.Response.WriteAsync(json);

               
            }
        }
    }
}