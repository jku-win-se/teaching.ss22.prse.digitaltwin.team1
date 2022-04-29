using SmartRoom.DataSimulatorService.Contracts;

namespace SmartRoom.DataSimulatorService.Models
{
    public abstract class Sensor<T> : ISensor
    {
        public T? Value { get; set; }
        public string Type { get; set; } = string.Empty;
        public abstract void RenewData();
        public override string ToString()
        {
            return $"[Sensor] [{Type}: {Value}]";
        }
    }
}
