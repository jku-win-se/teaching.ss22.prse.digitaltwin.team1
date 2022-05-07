using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartRoom.CommonBase.Utils;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SmartRoom.CommonBase.Web
{
    public class StartUpConfigManager
    {
        private readonly WebApplication _app;
        private const string _policyName = "CorsPolicy";
        public StartUpConfigManager(WebApplication app)
        {
            _app = app;
        }

        public void ConfigureApp()
        {
            _app.UseHttpsRedirection();

            _app.UseCors(_policyName);

            _app.UseAuthorization();
            _app.UseAuthentication();

            _app.MapControllers().RequireCors(_policyName);

            if (_app.Environment.IsDevelopment())
            {
                _app.UseSwagger();
                _app.UseSwaggerUI();
            }
            else
            {
                _app.UseSwagger();
                _app.UseSwaggerUI();
                _app.UseMiddleware<ApiKeyManager>();
            }
        }

        private static OpenApiSecurityScheme GetSwaggerSecurityDefinition()
        {
            return new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "ApiKey",
                Type = SecuritySchemeType.ApiKey
            };
        }

        private static OpenApiSecurityRequirement GetOpenApiSecurityRequirement()
        {
            return new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    new string[] {}
                }
            };
        }
        public static void SetAllowAnyCorsOptions(CorsOptions opt)
        {
            opt.AddPolicy(name: _policyName, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        }
        public static IConfigurationRoot GetConfigBuilder()
        {
            return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build()
            .Decrypt("CipherText:");
        }

        public static void SetSwaggerOptions(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("ApiKey", GetSwaggerSecurityDefinition());
            options.AddSecurityRequirement(GetOpenApiSecurityRequirement());
        }
    }
}
