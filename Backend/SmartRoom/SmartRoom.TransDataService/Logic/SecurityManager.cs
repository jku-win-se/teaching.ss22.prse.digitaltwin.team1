using Microsoft.AspNetCore.SignalR;
using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Transfer.Contracts;
using SmartRoom.TransDataService.Logic.Contracts;

namespace SmartRoom.TransDataService.Logic
{
    public class SecurityManager : ISecurityManager
    {
        private readonly IHubContext<SensorHub> _hub;
        private readonly IDataSimulatorContext _dataSimulatorContext;

        public SecurityManager(IHubContext<SensorHub> hub, IDataSimulatorContext dataSimulatorContext)
        {
            _hub = hub;
            _dataSimulatorContext = dataSimulatorContext;
        }

        public async void CheckTemperaturesAndSendAlarm(IEnumerable<IState?> states)
        {
            if (!states.Any()) return;

            await Task.Run(() =>
             states.Where(s => s!.Name.Equals("Temperature")).Select(s => s as MeasureState).Where(s => s?.Value >= 70).ToList()
                .ForEach(async s =>
                {
                    await _hub.Clients.All.SendAsync("Alarm", s);
                    await OpenAllDoorsOfRoom(s!);
                }
            ));
        }

        private async Task OpenAllDoorsOfRoom(MeasureState s)
        {
            await _dataSimulatorContext.SetAllBinariesForRoomByEqipmentType(s.EntityRefID, "Door", true);
        }
    }
}
