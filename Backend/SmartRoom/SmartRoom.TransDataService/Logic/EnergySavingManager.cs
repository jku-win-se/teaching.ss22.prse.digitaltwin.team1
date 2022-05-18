using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.TransDataService.Logic
{
    public class EnergySavingManager
    {
        private readonly ReadManager _readManager;
        private readonly IConfiguration _configuration;

        private string _baseDataServiceURL => _configuration["Services:BaseDataService"];
        private string _dataSimulatorURL => _configuration["Services:DataSimulatorService"];
        private string _apiKey => _configuration["ApiKey"];

        public EnergySavingManager(ReadManager readManager, IConfiguration configuration)
        {
            _readManager = readManager;
            _configuration = configuration;
        }
        //Energy Saving: Turn lights on if there are people in the room.
        public void TurnLightsOnPeopleInRoom(IEnumerable<IState> states)
        {
            states.Where(s => s.Name.Equals("PeopleInRoom")).Select(s => s as MeasureState)
                .Where(s => s?.Value > 0 &&
                 _readManager.GetStatesByEntityID<MeasureState>(s!.EntityRefID).GetAwaiter().GetResult().Take(2).ToArray()[1].Value == 0 &&
                 DateTime.Now.Hour <= 8 && DateTime.Now.Hour >= 19)
                .ToList().ForEach(async s =>
                {
                    await CommonBase.Utils.WebApiTrans.GetAPI<object>($"{_dataSimulatorURL}command/SetAllBianriesForRoomByEquipmentType/{s?.EntityRefID}&Light&true", _apiKey);
                });
        }

        // Energy Saving: Lights should be turned off if the room is empty.
        public void TurnLightsOffNoPeopleInRoom(IEnumerable<IState> states)
        {
            states.Where(s => s.Name.Equals("PeopleInRoom")).Select(s => s as MeasureState).Where(s => s?.Value == 0)
                .ToList().ForEach(async s =>
                {
                    await CommonBase.Utils.WebApiTrans.GetAPI<object>($"{_dataSimulatorURL}command/SetAllBianriesForRoomByEquipmentType/{s?.EntityRefID}&Light&false", _apiKey);
                });
        }

        //Energy Saving: Turn off running devices if the room is empty.
        public void TurnDevicesOffNoPeopleInRoom(IEnumerable<IState> states)
        {
            states.Where(s => s.Name.Equals("PeopleInRoom")).Select(s => s as MeasureState).Where(s => s?.Value == 0)
                .ToList().ForEach(async s =>
                {
                    await CommonBase.Utils.WebApiTrans.GetAPI<object>($"{_dataSimulatorURL}command/SetAllBianriesForRoomByEquipmentType/{s?.EntityRefID}&Ventilator&false", _apiKey);
                });
        }
    }
}
