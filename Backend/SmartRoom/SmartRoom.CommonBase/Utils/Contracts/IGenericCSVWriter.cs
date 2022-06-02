namespace SmartRoom.CommonBase.Utils.Contracts
{
    public interface IGenericCSVWriter : IDisposable
    {
        string? FileName { get; }
        string WriteToCSV();
    }
}