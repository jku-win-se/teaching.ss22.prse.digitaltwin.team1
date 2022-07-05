using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Models;
using Moq;
using SmartRoom.CommonBase.Web;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class StartUpConfigManagerTest
    {
        [Fact]
        public void Ctor_ValidParam_ValidObject()
        {
            var app = WebApplication.CreateBuilder().Build();

            Assert.NotNull(new StartUpConfigManager(app));
        }

        [Fact]
        public void ConfigureApp_NoException()
        {
            var mock = new Mock<IStartUpConfigManager>();

            try
            {
                mock.Object.ConfigureApp();
            }
            catch (System.Exception e)
            {
                Assert.Null(e);
            }
        }

        [Fact]
        public void GetSwaggerSecurityDefinition_ValidScheme()
        {
            Assert.Equal("ApiKey", StartUpConfigManager.GetSwaggerSecurityDefinition().Name);
            Assert.Equal(ParameterLocation.Header, StartUpConfigManager.GetSwaggerSecurityDefinition().In);
            Assert.Equal(SecuritySchemeType.ApiKey, StartUpConfigManager.GetSwaggerSecurityDefinition().Type);
        }

        [Fact]
        public void GetOpenApiSecurityRequirement_ValidOpenApiSecurityRequirement()
        {
            Assert.Equal("ApiKey", StartUpConfigManager.GetOpenApiSecurityRequirement().Keys.First().Reference.Id);
            Assert.Equal(ReferenceType.SecurityScheme, StartUpConfigManager.GetOpenApiSecurityRequirement().Keys.First().Reference.Type);
            Assert.IsType<string[]>(StartUpConfigManager.GetOpenApiSecurityRequirement().Values.First());
        }

        [Fact]
        public void SetAllowAnyCorsOptions_CorsOptions_SetsValidConfig()
        {
            var cors = new CorsOptions();

            StartUpConfigManager.SetAllowAnyCorsOptions(cors);

            Assert.True(cors.GetPolicy("CorsPolicy")?.AllowAnyMethod);
            Assert.True(cors.GetPolicy("CorsPolicy")?.AllowAnyHeader);
            Assert.True(cors.GetPolicy("CorsPolicy")?.Origins.Contains("http://localhost:4200"));
            Assert.True(cors.GetPolicy("CorsPolicy")?.Origins.Contains("http://localhost:3000"));
        }

        [Fact]
        public void GetConfigBuilder_Builder()
        {
            var builder = StartUpConfigManager.GetConfigBuilder();
            Assert.Equal(2, builder.Providers.Count());
        }

        [Fact]
        public void SetSwaggerOptions_ValidSwaggerOptions_SetsValidOptions()
        {
            var opt = new SwaggerGenOptions();

            StartUpConfigManager.SetSwaggerOptions(opt);

            Assert.Single(opt.SwaggerGeneratorOptions.SecuritySchemes);
            Assert.Single(opt.SwaggerGeneratorOptions.SecurityRequirements);
        }
    }
}
