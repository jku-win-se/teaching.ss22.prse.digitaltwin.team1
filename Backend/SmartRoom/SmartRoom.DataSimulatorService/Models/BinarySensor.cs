using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.DataSimulatorService.Models
{
    public class BinarySensor : Sensor<bool>
    {
        public BinarySensor(EventHandler handler, BinaryState state) : base(handler, state)
        { 
        }
        public override void ChangeState(DateTime timeStamp = default)
        {
            base.ChangeState(timeStamp);
            State.Value = !State.Value;
            OnStateUpdate();
        }
    }
}
