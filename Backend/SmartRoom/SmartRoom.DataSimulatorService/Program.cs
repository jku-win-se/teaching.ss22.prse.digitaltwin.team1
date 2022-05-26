using Serilog;
using SmartRoom.CommonBase.Transfer;
using SmartRoom.CommonBase.Web;
using SmartRoom.DataSimulatorService.Logic;

var builder = WebApplication.CreateBuilder(args);
DataSink sink = new();

Log.Logger = new LoggerConfiguration()
  .WriteTo.Sink(sink)
  .CreateLogger();

builder.Services.AddSingleton<IConfiguration>(StartUpConfigManager.GetConfigBuilder());
builder.Services.AddSingleton<DataSink>(sink);
builder.Services.AddSingleton<SensorManager, SensorManager>();
builder.Services.AddSingleton<ServiceRoutesManager, ServiceRoutesManager>();

builder.Services.AddHostedService<SimulatorService>();

builder.Services.AddTransient<TransDataServiceContext, TransDataServiceContext>();
builder.Services.AddTransient<BaseDataServiceContext, BaseDataServiceContext>();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddControllers();
builder.Services.AddCors(opt => StartUpConfigManager.SetAllowAnyCorsOptions(opt));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => StartUpConfigManager.SetSwaggerOptions(options));


WebApplication app = builder.Build();
StartUpConfigManager startUpManager = new (app);
startUpManager.ConfigureApp();

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

