using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Transfer;
using SmartRoom.TransDataService.Logic.Contracts;

namespace SmartRoom.TransDataService.Logic
{
    public class EnergySavingManager : IEnergySavingManager
    {
        private readonly IReadManager _readManager;
        private readonly DataSimulatorContext _dataSimulatorContext;
        public EnergySavingManager(IReadManager readManager, DataSimulatorContext dataSimulatorContext)
        {
            _readManager = readManager;
            _dataSimulatorContext = dataSimulatorContext;
        }
        //Energy Saving: Turn lights on if there are people in the room.
        public void TurnLightsOnPeopleInRoom(IEnumerable<IState> states)
        {
            if (DateTime.Now.Hour <= 8 || DateTime.Now.Hour >= 19)
            {
                states.Where(s => s.Name.Equals("PeopleInRoom")).Select(s => s as MeasureState)
                .Where(s => s?.Value > 0 &&
                 _readManager.GetStatesByEntityID<MeasureState>(s!.EntityRefID).GetAwaiter().GetResult().Take(2).ToArray()[1].Value == 0)
                .ToList().ForEach(async s =>
                {
                    await _dataSimulatorContext.SetAllBinariesForRoomByEqipmentType(s!.EntityRefID, "Light", true);
                });
            }
        }

        // Energy Saving: Lights should be turned off if the room is empty.
        public void TurnLightsOffNoPeopleInRoom(IEnumerable<IState> states)
        {
            states.Where(s => s.Name.Equals("PeopleInRoom")).Select(s => s as MeasureState).Where(s => s?.Value == 0)
                .ToList().ForEach(async s =>
                {
                    await _dataSimulatorContext.SetAllBinariesForRoomByEqipmentType(s!.EntityRefID, "Light", false);
                });
        }

        //Energy Saving: Turn off running devices if the room is empty.
        public void TurnDevicesOffNoPeopleInRoom(IEnumerable<IState> states)
        {
            states.Where(s => s.Name.Equals("PeopleInRoom")).Select(s => s as MeasureState).Where(s => s?.Value == 0)
                .ToList().ForEach(async s =>
                {
                    await _dataSimulatorContext.SetAllBinariesForRoomByEqipmentType(s!.EntityRefID, "Ventilator", false);
                });
        }
    }
}
