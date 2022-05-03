using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CSVConsole.Models
{
    public class VentilatorOn : IBaseModel<BinaryState>
    {
        public DateTime Timestamp { get; set; }
        public int VentilatorId { get; set; }
        public bool isOn { get; set; }

        public BinaryState GetEntity()
        {
            return new BinaryState
            {
                Value = isOn,
                TimeStamp = Timestamp,
                Name = typeof(VentilatorOn).Name
            };
        }
    }
}
