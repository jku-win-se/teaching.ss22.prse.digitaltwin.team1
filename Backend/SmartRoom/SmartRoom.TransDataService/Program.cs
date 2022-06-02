using Microsoft.EntityFrameworkCore;
using Npgsql;
using SmartRoom.CommonBase.Transfer;
using SmartRoom.CommonBase.Transfer.Contracts;
using SmartRoom.CommonBase.Web;
using SmartRoom.TransDataService.Logic;
using SmartRoom.TransDataService.Logic.Contracts;
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
builder.Services.AddSingleton<IServiceRoutesManager, ServiceRoutesManager>();

builder.Services.AddSingleton<IStateActions>(x =>
{
    return new StateActionsBuilder(x.GetRequiredService<IServiceProvider>())
    .SecurityActions()
    .EnergySavingActions()
    .AirQualityActions()
    .Build();
});

builder.Services.AddTransient<IReadManager, ReadManager>();
builder.Services.AddTransient<IDataSimulatorContext, DataSimulatorContext>();
builder.Services.AddTransient<IWriteManager, WriteManager>();
builder.Services.AddTransient<ISecurityManager, SecurityManager>();
builder.Services.AddTransient<IEnergySavingManager, EnergySavingManager>();
builder.Services.AddTransient<IAirQualityManager, AirQualityManager>();

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddCors(opt => StartUpConfigManager.SetAllowAnyCorsOptions(opt));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => StartUpConfigManager.SetSwaggerOptions(options));

WebApplication app = builder.Build();
IStartUpConfigManager startUpManager = new StartUpConfigManager(app);

app.UseDefaultFiles();
app.UseStaticFiles();
startUpManager.ConfigureApp();

app.MapHub<SensorHub>("/hub");

app.Run();
