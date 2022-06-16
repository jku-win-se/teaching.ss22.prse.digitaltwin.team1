using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.DataSimulatorService.Models.Contracts;

namespace SmartRoom.DataSimulatorService.Models
{
    public abstract class Sensor<T> : ISensor
    {
        public Sensor(EventHandler handler, State<T> state)
        {
            State = state;
            StateUpdated += handler;
        }
        public State<T> State { get; private set; } = new State<T>();
        public virtual void ChangeState(DateTime timeStamp = default) 
        {
            if(timeStamp == default) State.TimeStamp = DateTime.UtcNow;
            else State.TimeStamp = timeStamp;
        }
        public override string ToString()
        {
            return $"[Sensor: {this.State.EntityRefID}] [{State.Name}: {State.Value}]";
        }
        protected virtual void OnStateUpdate() 
        {
            EventHandler? handler = StateUpdated;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        public event EventHandler? StateUpdated;
    }
}
