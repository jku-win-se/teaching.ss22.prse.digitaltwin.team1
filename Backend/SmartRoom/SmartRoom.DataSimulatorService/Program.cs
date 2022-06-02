using Serilog;
using SmartRoom.CommonBase.Transfer;
using SmartRoom.CommonBase.Transfer.Contracts;
using SmartRoom.CommonBase.Web;
using SmartRoom.DataSimulatorService.Logic;
using SmartRoom.DataSimulatorService.Logic.Contracts;

var builder = WebApplication.CreateBuilder(args);
IDataSink sink = new DataSink();

Log.Logger = new LoggerConfiguration()
  .WriteTo.Sink(sink)
  .CreateLogger();

builder.Services.AddSingleton<IConfiguration>(StartUpConfigManager.GetConfigBuilder());
builder.Services.AddSingleton<IDataSink>(sink);
builder.Services.AddSingleton<ISensorManager, SensorManager>();
builder.Services.AddSingleton<IServiceRoutesManager, ServiceRoutesManager>();

builder.Services.AddHostedService<SimulatorService>();

builder.Services.AddTransient<ITransDataServiceContext, TransDataServiceContext>();
builder.Services.AddTransient<IBaseDataServiceContext, BaseDataServiceContext>();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddControllers();
builder.Services.AddCors(opt => StartUpConfigManager.SetAllowAnyCorsOptions(opt));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => StartUpConfigManager.SetSwaggerOptions(options));


WebApplication app = builder.Build();
IStartUpConfigManager startUpManager = new StartUpConfigManager(app);
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

