using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.TransDataService.Logic.Contracts;

namespace SmartRoom.TransDataService.Logic
{
    public class StateActionsBuilder : IStateActionsBuilder
    {
        private Action<IEnumerable<IState>>? _actions;
        private readonly IServiceProvider _serviceProvider;

        private ISecurityManager? _securityManager;
        private IAirQualityManager? _airQualityManager;
        private IEnergySavingManager? _energySavingManager;

        public StateActionsBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public StateActionsBuilder SecurityActions()
        {
            _securityManager = _serviceProvider.GetService<ISecurityManager>()!;
            _actions += _securityManager.CheckTemperaturesAndSendAlarm;
            return this;
        }
        public StateActionsBuilder EnergySavingActions()
        {
            _energySavingManager = _serviceProvider.GetService<IEnergySavingManager>()!;
            _actions += _energySavingManager.TurnLightsOnPeopleInRoom;
            _actions += _energySavingManager.TurnLightsOffNoPeopleInRoom;
            _actions += _energySavingManager.TurnDevicesOffNoPeopleInRoom;
            return this;
        }
        public StateActionsBuilder AirQualityActions()
        {
            _airQualityManager = _serviceProvider.GetService<IAirQualityManager>()!;
            _actions += _airQualityManager.CheckCo2ImporveAirQuality;
            return this;
        }

        public StateActions Build()
        {
            return new StateActions(_actions!);
        }
    }
}
