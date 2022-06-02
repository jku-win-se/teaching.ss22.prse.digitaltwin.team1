namespace SmartRoom.DataSimulatorService.Models.Contracts
{
    public interface ISensor
    {
        public event EventHandler? StateUpdated;
        public void ChangeState(DateTime dateTime = default);
    }
}
