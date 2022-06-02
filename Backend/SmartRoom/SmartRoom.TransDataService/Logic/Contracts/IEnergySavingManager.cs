using SmartRoom.CommonBase.Core.Contracts;

namespace SmartRoom.TransDataService.Logic.Contracts
{
    public interface IEnergySavingManager
    {
        void TurnDevicesOffNoPeopleInRoom(IEnumerable<IState> states);
        void TurnLightsOffNoPeopleInRoom(IEnumerable<IState> states);
        void TurnLightsOnPeopleInRoom(IEnumerable<IState> states);
    }
}