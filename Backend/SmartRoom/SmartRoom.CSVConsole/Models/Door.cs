using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CSVConsole.Models
{
    public class Door : IBaseModel<RoomEquipment>
    {
        public int ID { get; set; }
        public RoomEquipment GetEntity()
        {
            return new RoomEquipment
            {
                EquipmentRef = ID.ToString(),
                Name = typeof(Door).Name
            };
        }
    }
}
