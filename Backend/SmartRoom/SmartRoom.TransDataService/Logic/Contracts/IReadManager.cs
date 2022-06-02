using SmartRoom.CommonBase.Core.Contracts;

namespace SmartRoom.TransDataService.Logic.Contracts
{
    public interface IReadManager
    {
        Task<object> GetChartData<E>(Guid id, string name, int intervall, int daySpan) where E : class, IState;
        Task<object> GetChartData<E>(Guid[] ids, string name, int intervall, int daySpan) where E : class, IState;
        Task<E> GetRecentStateByEntityID<E>(Guid id, string name) where E : class, IState, new();
        Task<E[]> GetStatesByEntityID<E>(Guid id) where E : class, IState;
        Task<E[]> GetStatesByTimeSpan<E>(DateTime from, DateTime to) where E : class, IState;
        Task<string[]> GetStateTypesByEntityID<E>(Guid id) where E : class, IState;
    }
}