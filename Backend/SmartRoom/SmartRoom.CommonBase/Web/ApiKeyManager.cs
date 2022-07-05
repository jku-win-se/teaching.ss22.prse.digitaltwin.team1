using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SmartRoom.CommonBase.Web.Contracts;

namespace SmartRoom.CommonBase.Web
{
    public class ApiKeyManager : IApiKeyManager
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private const string APIKEY = "ApiKey";
        public ApiKeyManager(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var apiKey = _configuration["ApiKey"];
            if (!context.Request.Headers.TryGetValue(APIKEY, out
                    var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided ");
                return;
            }


            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }
            await _next(context);
        }
    }
}
