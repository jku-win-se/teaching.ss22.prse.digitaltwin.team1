using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CSVConsole.Models

{
    public class Window : IBaseModel<RoomEquipment>
    {
        public int ID { get; set; }
        public string Room_Id { get; set; } = string.Empty; 

        public RoomEquipment GetEntity()
        {
            return new RoomEquipment
            {
                Name = typeof(Window).Name,
                EquipmentRef = ID.ToString()
            };
        }
    }
}
