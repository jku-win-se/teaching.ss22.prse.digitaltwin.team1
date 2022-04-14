﻿using System.ComponentModel.DataAnnotations;

namespace SmartRoom.CommonBase.Core.Entities
{
    public class Room : EntityObject
    {
        [Required]
        public int PeopleCount { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public int Size { get; set; }
        public string RoomType { get; set; } = "Unknown";
        public string Building { get; set; } = "Unknown";
        public ICollection<RoomEquipment> RoomEquipment { get; set; } = new List<RoomEquipment>();
    }
}
