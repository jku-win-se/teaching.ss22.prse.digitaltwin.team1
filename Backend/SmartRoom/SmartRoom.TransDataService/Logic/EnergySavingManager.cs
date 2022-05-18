using SmartRoom.CommonBase.Core.Contracts;

namespace SmartRoom.TransDataService.Logic
{
    public class EnergySavingManager
    {
        //Energy Saving: Turn lights on if there are people in the room.
        public async void TurnLightsOnPeopleInRoom(IEnumerable<IState> states)
        {

        }

        // Energy Saving: Lights should be turned off if the room is empty.
        public async void TurnLightsOffNoPeopleInRoom(IEnumerable<IState> states)
        {

        }

        //Energy Saving: Turn off running devices if the room is empty.
        public async void TurnDevicesOffNoPeopleInRoom(IEnumerable<IState> states)
        {

        }
    }
}
