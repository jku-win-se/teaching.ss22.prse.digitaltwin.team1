using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Transfer.Contracts;
using SmartRoom.TransDataService.Logic.Contracts;

namespace SmartRoom.TransDataService.Logic
{
    public class AirQualityManager : IAirQualityManager
    {
        private readonly IDataSimulatorContext _dataSimulatorContext;
        public AirQualityManager(IDataSimulatorContext dataSimulatorContext)
        {
            _dataSimulatorContext = dataSimulatorContext;
        }

        //Air Quality: Open window + activate fan if co2 values are > 1000 parts per million (ppm).
        public async void CheckCo2ImporveAirQuality(IEnumerable<IState> states)
        {
            if (states == null || !states.Any()) return;

            await Task.Run(() =>
            states.Where(s => s!.Name.Equals("Co2")).Select(s => s as MeasureState).Where(s => s?.Value > 1000).ToList()
                .ForEach(async s =>
                {
                    await OpenWindowsByState(s!);
                    await RunFansByState(s!);
                }
            ));
        }

        public async Task OpenWindowsByState(IState s)
        {
            await _dataSimulatorContext.SetAllBinariesForRoomByEqipmentType(s.EntityRefID, "Window", true);
        }

        public async Task RunFansByState(IState s)
        {
            await _dataSimulatorContext.SetAllBinariesForRoomByEqipmentType(s.EntityRefID, "Ventilator", true);
        }
    }
}
