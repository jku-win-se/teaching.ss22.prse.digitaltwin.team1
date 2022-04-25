using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using SmartRoom.CommonBase.Utils;
using SmartRoom.CommonBase.Web;
using SmartRoom.DataSimulatorService.Logic;

string _policyName = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);

DataSink sink = new DataSink();

Log.Logger = new LoggerConfiguration()
  .WriteTo.Console()
  .WriteTo.File(Directory.GetCurrentDirectory() + "Logs.txt", shared: true)
  .WriteTo.Sink(sink)
  .CreateLogger();


var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build()
            .Decrypt("CipherText:");

builder.Services.AddSingleton<IConfiguration>(configBuilder);
builder.Services.AddSingleton<DataSink>(sink);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

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

app.UseHttpsRedirection();

app.UseCors(_policyName);

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers().RequireCors(_policyName);

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

try
{
    Log.Information("Starting up");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

