namespace SmartRoom.DataSimulatorService.Logic
{
    public class SimulatorService : BackgroundService
    {
        private Timer _timer = null!;
        private readonly ILogger _logger;
        private readonly SensorManager _sensorManager;
        private readonly IHostApplicationLifetime _host;
        private bool _timerIsOn;

        public SimulatorService(ILogger<SimulatorService> logger, SensorManager dataManager, IHostApplicationLifetime host)
        {
            _logger = logger;
            _sensorManager = dataManager;
            _host = host;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("[Simulator] [Starting Service]");
            _sensorManager.Init().GetAwaiter().GetResult();
            _logger.LogInformation("[Simulator] [Generating Missing Data]");
            _sensorManager.GenerateMissingData().GetAwaiter().GetResult();

            var timer = new System.Timers.Timer(TimeSpan.FromMinutes(10).TotalMilliseconds) { AutoReset = false };
            timer.Elapsed += (s, e) => StopService();
            timer.Enabled = true;

            _logger.LogInformation("[Simulator] [Running]");
            StartTimer();

            return Task.CompletedTask;
        }

        private void StopService()
        {
            _logger.LogInformation("[Simulator] [Stopped]");
            _host.StopApplication();
        }

        private void StartTimer() 
        {
            if (_timer == null) _timer = new Timer(RunSimulation, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5));
            else if (!_timerIsOn) _timer.Change(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(60));
            _timerIsOn = true;
        }

        private void RunSimulation(object? state)
        {
            _sensorManager.GenerateData();
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("[Simulator] [Stopping Service]");

            StopService();

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
