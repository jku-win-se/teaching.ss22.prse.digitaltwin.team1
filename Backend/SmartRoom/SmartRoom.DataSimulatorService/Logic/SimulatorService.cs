namespace SmartRoom.DataSimulatorService.Logic
{
    public class SimulatorService : BackgroundService
    {
        private Timer _timer = null!;
        private ILogger _logger;

        public SimulatorService(ILogger<SimulatorService> logger)
        {
            _logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("[Simulator] [Starting Service]");
            _timer = new Timer(RunSimulation, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void RunSimulation(object? state)
        {
            _logger.LogInformation($"[Simulator] [Starting Data Generator]");
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
