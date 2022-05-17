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
            return this;
        }
        public StateActionsBuilder AirQualityActions()
        {
            _airQualityManager = _serviceProvider.GetService<AirQualityManager>()!;
            return this;
        }

        public StateActions Build() 
        {
            return new StateActions(_actions!);
        }
    }
}
