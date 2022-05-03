using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.DataSimulatorService.Models
{
    public class BinarySensor : Sensor<bool>
    {
        public BinarySensor()
        {

        }   
        public BinarySensor(BinaryState state)
        {
            Value = state.Value;
            TimeStamp = state.TimeStamp;
            Type = state.Name;
        }
        public override void ChangeState(DateTime timeStamp = default)
        {
            base.ChangeState(timeStamp);
            Value = !Value;
        }
    }
}
