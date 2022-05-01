namespace SmartRoom.DataSimulatorService.Contracts
{
    public interface ISensor
    {
        public string Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public void ChangeState();
    }
}
