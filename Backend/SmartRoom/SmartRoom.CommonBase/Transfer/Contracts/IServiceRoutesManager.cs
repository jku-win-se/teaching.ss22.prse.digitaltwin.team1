namespace SmartRoom.CommonBase.Transfer.Contracts
{
    public interface IServiceRoutesManager
    {
        string ApiKey { get; }
        string BaseDataService { get; }
        string DataSimulatorService { get; }
        string TransDataService { get; }
    }
}