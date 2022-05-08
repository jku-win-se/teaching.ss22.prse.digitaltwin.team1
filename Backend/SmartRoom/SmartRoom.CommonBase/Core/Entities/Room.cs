using System.ComponentModel.DataAnnotations;

namespace SmartRoom.CommonBase.Core.Entities
{

    public class Room : EntityObject
    {
        [Required]
        public int PeopleCount { get; set; }   
        public string Name { get; set; } = string.Empty;
        public int Size { get; set; }
        public string RoomType { get; set; } = "Unknown";
        public string Building { get; set; } = "Unknown";
        public List<RoomEquipment> RoomEquipment { get; set; } = new List<RoomEquipment>();
    }
}
