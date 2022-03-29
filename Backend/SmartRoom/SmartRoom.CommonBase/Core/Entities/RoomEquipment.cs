using System.ComponentModel.DataAnnotations;

namespace SmartRoom.CommonBase.Core.Entities
{
    public class RoomEquipment : EntityObject
    {
        public int RoomID { get; set; }
        [Required]
        public string EquipmentRef { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ICollection<State> States { get; set; } = new List<State>();
    }
}
