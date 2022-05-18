using SmartRoom.CommonBase.Core.Contracts;

namespace SmartRoom.TransDataService.Logic
{
    public class AirQualityManager
    {
        //Air Quality: Open window + activate fan if co2 values are > 1000 parts per million (ppm).
        public async void CheckCo2ImporveAitQuality(IEnumerable<IState> states) 
        {
            
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
