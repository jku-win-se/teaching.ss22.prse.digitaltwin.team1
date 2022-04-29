namespace SmartRoom.DataSimulatorService.Logic
{
    public class SimulatorService : BackgroundService
    {
        private Timer _timer = null!;
        private ILogger _logger;
        private DataManager _dataManager;

        public SimulatorService(ILogger<SimulatorService> logger, DataManager dataManager)
        {
            _logger = logger;
            _dataManager = dataManager;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("[Simulator] [Starting Service]");
            _dataManager.LoadData().GetAwaiter().GetResult();
            _timer = new Timer(RunSimulation, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void RunSimulation(object? state)
        {
            _logger.LogInformation($"[Simulator] [Starting Data Generator]");
            _dataManager.GenerateData().GetAwaiter().GetResult();
            _logger.LogInformation($"[Simulator] [Stopping Data Generator]");
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("[Simulator] [Stopping Service]");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
