using SmartRoom.CommonBase.Core.Contracts;

namespace SmartRoom.TransDataService.Logic.Contracts
{
    public interface IStateActions
    {
        void RunActions(IEnumerable<IState> states);
    }
}