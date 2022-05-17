using SmartRoom.CommonBase.Core.Contracts;

namespace SmartRoom.TransDataService.Logic
{
    public class StateActions
    {
        private Action<IEnumerable<IState>>? _actions;
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
