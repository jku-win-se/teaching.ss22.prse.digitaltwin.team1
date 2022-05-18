using Microsoft.AspNetCore.SignalR;
using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.TransDataService.Logic
{
    public class SecurityManager
    {
        private readonly IHubContext<SensorHub> _hub;

        private string _baseDataServiceURL => _configuration["Services:BaseDataService"];
        private string _dataSimulatorURL => _configuration["Services:DataSimulatorService"];
        private string _apiKey => _configuration["ApiKey"];
        private readonly IConfiguration _configuration;

        public SecurityManager(IHubContext<SensorHub> hub, IConfiguration configuration)
        {
            _hub = hub;
            _configuration = configuration;
        }

        public async void CheckTemperaturesAndSendAlarm(IEnumerable<IState?> states)
        {
            if (!states.Any()) return;

            await Task.Run(() =>
             states.Where(s => s!.Name.Equals("Temperature")).Select(s => s as MeasureState).Where(s => s?.Value >= 70).ToList()
                .ForEach(async s =>
                {
                    await _hub.Clients.All.SendAsync("Alarm", s);
                    await OpenAllDoorsOfRoom(s);
                }
            ));
        }

        private async Task OpenAllDoorsOfRoom(MeasureState? s)
        {
            var rooms = await CommonBase.Utils.WebApiTrans.GetAPI<List<Room>>($"{_baseDataServiceURL}room", _apiKey);
            var room = rooms.First(r => r.Id.Equals(s?.EntityRefID));
            await CommonBase.Utils.WebApiTrans.GetAPI<object>($"{_dataSimulatorURL}command/SetAllBianriesForRoomByEquipmentType/{room.Id}&Door&true", _apiKey);
        }
    }
}
