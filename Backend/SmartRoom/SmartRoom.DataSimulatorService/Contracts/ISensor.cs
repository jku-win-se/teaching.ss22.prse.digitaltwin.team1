namespace SmartRoom.DataSimulatorService.Contracts
{
    public interface ISensor
    {
        public event EventHandler? StateUpdated;
        public void ChangeState(DateTime dateTime = default);
    }
}
