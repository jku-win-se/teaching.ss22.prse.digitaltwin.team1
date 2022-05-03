using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CSVConsole.Models
{
    public class WindowOpen : IBaseModel<BinaryState>
    {
        public DateTime Timestamp { get; set; }
        public int Window_ID { get; set; }
        public bool isOpen { get; set; }

        public BinaryState GetEntity()
        {
            return new BinaryState
            {
                Value = isOpen,
                Name = typeof(WindowOpen).Name,
                TimeStamp = Timestamp
            };
        }
    }
}
