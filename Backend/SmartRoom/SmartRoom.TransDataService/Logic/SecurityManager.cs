using Microsoft.AspNetCore.SignalR;
using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.TransDataService.Logic
{
    public class SecurityManager
    {
        private readonly IHubContext<SensorHub> _hub;

        private string _baseDataServiceURL => _configuration["Services:BaseDataService"];
        private string _apiKey => _configuration["ApiKey"];
        private readonly IConfiguration _configuration;

        public SecurityManager(IHubContext<SensorHub> hub, IConfiguration configuration)
        {
            _hub = hub;
            _configuration = configuration;
        }
 
        public async Task CheckTemperaturesAndSendAlarm(IEnumerable<MeasureState?> tempStates)
        {
            if (!tempStates.Any()) return;
            await Task.Run(() =>
            tempStates.Where(s => s?.Value >= 70).ToList()
                .ForEach(async s =>
                {
                    await _hub.Clients.All.SendAsync("Alarm", s);
                    //await OpenAllDoorsOfRoom(s);
                }
            ));
        }

        private async Task OpenAllDoorsOfRoom(MeasureState? s)
        {
            var rooms = await CommonBase.Utils.WebApiTrans.GetAPI<List<Room>>($"{_baseDataServiceURL}room", _apiKey);
            var doors = rooms.First(r => r.Id.Equals(s?.EntityRefID)).RoomEquipment.Where(rq => rq.Name.Equals("Door"));
        }
    }
}
