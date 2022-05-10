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
            double rnd = random.NextDouble();

            if (State.Value <= 18 && State.Name.Equals("Temperature")) rnd *= 1;
            else if (State.Value >= 30 && State.Name.Equals("Temperature")) rnd *= -1;
            else if (State.Value <= 50 && State.Name.Equals("Co2")) rnd += random.Next(1, 10);
            else if (State.Value >= 3000 && State.Name.Equals("Co2")) rnd -= random.Next(1, 10);
            else
            {
                if (State.TimeStamp > DateTime.Parse("08:00") && State.TimeStamp < DateTime.Parse("20:00"))
                {
                    if (random.Next(1, 10) < 4) rnd *= -1;
                }
                else 
                {
                    if (random.Next(1, 10) > 4) rnd *= -1;
                }
            }

            State.Value += rnd;
        }
    }
}
