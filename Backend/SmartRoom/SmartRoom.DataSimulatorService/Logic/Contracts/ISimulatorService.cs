namespace SmartRoom.DataSimulatorService.Logic.Contracts
{
    public interface ISimulatorService
    {
        void Dispose();
        Task StopAsync(CancellationToken stoppingToken);
    }
}