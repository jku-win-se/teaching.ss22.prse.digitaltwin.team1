using SmartRoom.CommonBase.Core.Contracts;

namespace SmartRoom.TransDataService.Logic.Contracts
{
    public interface IWriteManager
    {
        Task addState<E>(E[] states) where E : class, IState;
    }
}