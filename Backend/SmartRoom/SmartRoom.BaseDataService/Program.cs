using Microsoft.EntityFrameworkCore;
using Npgsql;
using SmartRoom.BaseDataService.Persistence;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Logic;
using SmartRoom.CommonBase.Logic.Contracts;
using SmartRoom.CommonBase.Persistence.Contracts;
using SmartRoom.CommonBase.Web;

var builder = WebApplication.CreateBuilder(args);
var configBuilder = StartUpConfigManager.GetConfigBuilder();

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
builder.Services.AddCors(opt => StartUpConfigManager.SetAllowAnyCorsOptions(opt));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => StartUpConfigManager.SetSwaggerOptions(options));

WebApplication app = builder.Build();
StartUpConfigManager startUpManager = new(app);
startUpManager.ConfigureApp();

app.Run();
