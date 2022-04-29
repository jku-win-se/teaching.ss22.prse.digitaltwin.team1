namespace SmartRoom.DataSimulatorService.Contracts
{
    public interface ISensor
    {
        public string Type { get; set; }
        public void RenewData();
    }
}
