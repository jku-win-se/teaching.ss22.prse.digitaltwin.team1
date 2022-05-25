using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.TransDataService.Logic
{
    public class AirQualityManager
    {

        
        private readonly IConfiguration _configuration;
        private string _dataSimulatorURL => _configuration["Services:DataSimulatorService"];
        private string _apiKey => _configuration["ApiKey"];

        public AirQualityManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Air Quality: Open window + activate fan if co2 values are > 1000 parts per million (ppm).
        public async void CheckCo2ImporveAitQuality(IEnumerable<IState> states) 
        {
            if (!states.Any()) return;

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
            await CommonBase.Utils.WebApiTrans.GetAPI<object>($"{_dataSimulatorURL}command/SetAllBianriesForRoomByEquipmentType/{s?.EntityRefID}&Window&true", _apiKey);
        }

        public async Task RunFansByState(IState s)
        {
            await CommonBase.Utils.WebApiTrans.GetAPI<object>($"{_dataSimulatorURL}command/SetAllBianriesForRoomByEquipmentType/{s?.EntityRefID}&Ventilator&true", _apiKey);
        }
    }
}
