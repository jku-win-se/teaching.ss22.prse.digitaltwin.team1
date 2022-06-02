using SmartRoom.TransDataService.Logic;

namespace SmartRoom.TransDataService.Logic.Contracts
{
    public interface IStateActionsBuilder
    {
        StateActionsBuilder AirQualityActions();
        StateActions Build();
        StateActionsBuilder EnergySavingActions();
        StateActionsBuilder SecurityActions();
    }
}