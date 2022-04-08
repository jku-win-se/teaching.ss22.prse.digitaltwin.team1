using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using SmartRoom.BaseDataService.Persistence;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Logic;
using SmartRoom.CommonBase.Logic.Contracts;
using SmartRoom.CommonBase.Persistence.Contracts;
using SmartRoom.CommonBase.Utils;
using SmartRoom.CommonBase.Web;

var builder = WebApplication.CreateBuilder(args);
var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build()
            .Decrypt("CipherText:");

// Add services to the container.
var npCpnn = new NpgsqlConnectionStringBuilder(configBuilder["DbConnection:ConnectionString"]);

builder.Services.AddDbContext<SmartRoomDBContext>(options =>
{
    options.UseNpgsql(npCpnn.ConnectionString);
});

builder.Services.AddScoped<IUnitOfWork, SmartRoomUOW>();
builder.Services.AddTransient<IGenericEntityManager<Room>, GenericEntityManager<Room>>();
builder.Services.AddTransient<IGenericEntityManager<RoomEquipment>, GenericEntityManager<RoomEquipment>>();
builder.Services.AddSingleton<IConfiguration>(configBuilder);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
