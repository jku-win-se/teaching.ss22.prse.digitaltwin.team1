namespace SmartRoom.CommonBase.Utils.Contracts
{
    public interface IGenericCSVReader<T> : IDisposable where T : new()
    {
        IEnumerable<T> Read();
    }
}