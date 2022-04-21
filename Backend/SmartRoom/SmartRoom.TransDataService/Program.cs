using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Utils;
using SmartRoom.CommonBase.Web;
using SmartRoom.TransDataService.Controllers;
using SmartRoom.TransDataService.Logic;
using SmartRoom.TransDataService.Persistence;

string _policyName = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);

var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build()
            .Decrypt("CipherText:");

// Add services to the container.
var npCpnn = new NpgsqlConnectionStringBuilder(configBuilder["DbConnection:ConnectionString"]);

builder.Services.AddDbContextFactory<TransDataDBContext>(options =>
{
    options.UseNpgsql(npCpnn.ConnectionString)
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); ;
});

builder.Services.AddSingleton<IConfiguration>(configBuilder);
builder.Services.AddTransient<ReadManager, ReadManager>();
builder.Services.AddTransient<WriteManager, WriteManager>();

builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: _policyName, builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var securityReq = new OpenApiSecurityRequirement()
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
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "ApiKey",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(securityReq);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ApiKeyManager>();
}

app.UseHttpsRedirection();

app.UseCors(_policyName);

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
