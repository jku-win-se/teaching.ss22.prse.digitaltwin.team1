using System.ComponentModel.DataAnnotations;

namespace SmartRoom.CommonBase.Core.Entities
{
    public class Room : EntityObject
    {
        [Required]
        public int Size { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<RoomEquipment> RoomEquipment { get; set; } = new List<RoomEquipment>();
        public ICollection<State> State { get; set; } = new List<State>();
    }
}
