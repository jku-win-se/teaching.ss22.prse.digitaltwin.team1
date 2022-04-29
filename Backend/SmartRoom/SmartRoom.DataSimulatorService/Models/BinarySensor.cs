namespace SmartRoom.DataSimulatorService.Models
{
    public class BinarySensor : Sensor<bool>
    {
        public override void RenewData()
        {
            Value = !Value;
        }
    }
}
