using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.TransDataService.Logic
{
    public class AirQualityManager
    {
        //Air Quality: Open window + activate fan if co2 values are > 1000 parts per million (ppm).
        public async void CheckCo2ImporveAitQuality(IEnumerable<IState> states) 
        {
            if (!states.Any()) return;

            await Task.Run(() =>
            states.Where(s => s!.Name.Equals("Airquality")).Select(s => s as MeasureState).Where(s => s?.Value > 1000).ToList()
                .ForEach(async s =>
                {
                    await OpenWindowsByState(s);
                    await RunFansByState(s);    
                }
            ));
            
        }

        public async Task OpenWindowsByState(IState state) 
        {
            Console.WriteLine(state.Name);
        }

        public async Task RunFansByState(IState state)
        {
            Console.WriteLine(state.Name);
        }
    }
}
