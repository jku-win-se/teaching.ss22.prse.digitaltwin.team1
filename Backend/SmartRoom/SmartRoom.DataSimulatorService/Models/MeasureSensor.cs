using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.DataSimulatorService.Models
{
    public class MeasureSensor : Sensor<double>
    {
        public MeasureSensor(EventHandler handler, MeasureState state) : base(handler, state)
        {
        }
        public override void ChangeState(DateTime timeStamp = default)
        {
            base.ChangeState(timeStamp);
            Random random = new Random();
            if (State.TimeStamp > DateTime.Parse("09:00") && State.TimeStamp < DateTime.Parse("19:00"))
            {
                if (random.Next(1, 10) > 4)
                {
                    State.Value += random.NextDouble();
                }
                else State.Value -= random.NextDouble();
            }
            else
            {
                if (random.Next(1, 10) > 4)
                {
                    State.Value -= random.NextDouble();
                }
                else State.Value += random.NextDouble();
            }
        }
    }
}
