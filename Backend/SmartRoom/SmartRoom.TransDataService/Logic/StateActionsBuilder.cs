using SmartRoom.CommonBase.Core.Contracts;

namespace SmartRoom.TransDataService.Logic
{
    public class StateActionsBuilder
    {
        private Action<IEnumerable<IState>>? _actions;
        private readonly IServiceProvider _serviceProvider;

        private SecurityManager? _securityManager;
        private AirQualityManager? _airQualityManager;
        private EnergySavingManager? _energySavingManager;

        public StateActionsBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public StateActionsBuilder SecurityActions() 
        {
            _securityManager = _serviceProvider.GetService<SecurityManager>()!;
            _actions += _securityManager.CheckTemperaturesAndSendAlarm;
            return this;
        }
        public StateActionsBuilder EnergySavingActions()
        {
            _energySavingManager = _serviceProvider.GetService<EnergySavingManager>()!;
            _actions += _energySavingManager.TurnLightsOnPeopleInRoom;
            _actions += _energySavingManager.TurnLightsOffNoPeopleInRoom;
            _actions += _energySavingManager.TurnDevicesOffNoPeopleInRoom;
            return this;
        }
        public StateActionsBuilder AirQualityActions()
        {
            _airQualityManager = _serviceProvider.GetService<AirQualityManager>()!;
            _actions += _airQualityManager.CheckCo2ImporveAitQuality;
            return this;
        }

        public StateActions Build() 
        {
            return new StateActions(_actions!);
        }
    }
}
