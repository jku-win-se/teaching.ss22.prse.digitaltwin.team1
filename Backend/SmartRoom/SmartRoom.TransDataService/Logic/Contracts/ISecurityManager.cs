using SmartRoom.CommonBase.Core.Contracts;

namespace SmartRoom.TransDataService.Logic.Contracts
{
    public interface ISecurityManager
    {
        void CheckTemperaturesAndSendAlarm(IEnumerable<IState?> states);
    }
}