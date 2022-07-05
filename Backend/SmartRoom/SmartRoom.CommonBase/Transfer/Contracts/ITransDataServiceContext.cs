using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CommonBase.Transfer.Contracts
{
    public interface ITransDataServiceContext
    {
        Task AddBinaryStates(State<bool>[] states);
        Task AddMeasureStates(State<double>[] states);
        Task<BinaryState> GetRecentBinaryStateBy(Guid entityId, string measureType);
        Task<MeasureState> GetRecentMeasureStateBy(Guid entityId, string measureType);
    }
}