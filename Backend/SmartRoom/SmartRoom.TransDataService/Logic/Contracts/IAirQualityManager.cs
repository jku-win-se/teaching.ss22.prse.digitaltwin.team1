using SmartRoom.CommonBase.Core.Contracts;

namespace SmartRoom.TransDataService.Logic.Contracts
{
    public interface IAirQualityManager
    {
        void CheckCo2ImporveAirQuality(IEnumerable<IState> states);
        Task OpenWindowsByState(IState s);
        Task RunFansByState(IState s);
    }
}