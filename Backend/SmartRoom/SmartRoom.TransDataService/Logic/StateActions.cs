using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.TransDataService.Logic.Contracts;

namespace SmartRoom.TransDataService.Logic
{
    public class StateActions : IStateActions
    {
        private readonly Action<IEnumerable<IState>>? _actions;
        public StateActions(Action<IEnumerable<IState>> actions)
        {
            _actions = actions;
        }

        public void RunActions(IEnumerable<IState> states)
        {
            _actions?.Invoke(states);
        }
    }
}
