namespace SmartRoom.DataSimulatorService.Models
{
    public class MeasureSensor : Sensor<double>
    {
        public override void RenewData()
        {
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
