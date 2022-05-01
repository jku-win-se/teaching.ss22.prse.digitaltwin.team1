using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.DataSimulatorService.Models
{
    public class MeasureSensor : Sensor<double>
    {
        public MeasureSensor()
        {

        }
        public MeasureSensor(MeasureState state)
        {
            Value = state.MeasureValue;
            TimeStamp = state.TimeStamp;
            Type = state.Name;
        }
        public override void ChangeState()
        {
            base.ChangeState();
            Random random = new Random();
            if (DateTime.Now > DateTime.Parse("09:00") && DateTime.Now < DateTime.Parse("19:00"))
            {
                if (random.Next(1, 10) > 3)
                {
                    Value += random.NextDouble();
                }
                else Value -= random.NextDouble();
            }
            else
            {
                if (random.Next(1, 10) > 3)
                {
                    Value -= random.NextDouble();
                }
                else Value += random.NextDouble();
            }
        }
    }
}
