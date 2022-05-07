using Microsoft.EntityFrameworkCore;
using Npgsql;
using SmartRoom.CommonBase.Web;
using SmartRoom.TransDataService.Logic;
using SmartRoom.TransDataService.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configBuilder = StartUpConfigManager.GetConfigBuilder();

var npCpnn = new NpgsqlConnectionStringBuilder(configBuilder["DbConnection:ConnectionString"]);

builder.Services.AddDbContextFactory<TransDataDBContext>(options =>
{
    options.UseNpgsql(npCpnn.ConnectionString)
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddSingleton<IConfiguration>(configBuilder);
builder.Services.AddTransient<ReadManager, ReadManager>();
builder.Services.AddTransient<WriteManager, WriteManager>();

builder.Services.AddControllers();
builder.Services.AddCors(opt => StartUpConfigManager.SetAllowAnyCorsOptions(opt));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => StartUpConfigManager.SetSwaggerOptions(options));

WebApplication app = builder.Build();
StartUpConfigManager startUpManager = new(app);
startUpManager.ConfigureApp();

app.Run();
