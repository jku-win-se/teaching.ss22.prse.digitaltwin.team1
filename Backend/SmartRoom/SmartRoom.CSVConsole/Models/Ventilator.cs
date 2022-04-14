using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CSVConsole.Models
{
    public class Ventilator : IBaseModel<RoomEquipment>
    {
        public string ID { get; set; } = String.Empty;
        public string Room_Id { get; set; } = String.Empty;

        public RoomEquipment GetEntity()
        {
            return new RoomEquipment
            {
                Name = typeof(Ventilator).Name,
                EquipmentRef = ID
            };
        }
    }
}
