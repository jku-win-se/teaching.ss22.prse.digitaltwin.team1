namespace SmartRoom.DataSimulatorService.Logic.Contracts
{
    public interface ISensorManager
    {
        void ChangeState(Guid id, string type);
        void GenerateData();
        Task GenerateMissingData();
        Task Init();
        void SetAllBinariesByRoom(Guid id, string type, bool val);
    }
}