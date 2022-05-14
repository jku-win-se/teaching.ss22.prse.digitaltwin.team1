using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Utils;
using System.ComponentModel.DataAnnotations;

namespace SmartRoom.BaseDataService.Models
{
    public class SpecialRoomModelForOurFEDev
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int PeopleCount { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Size { get; set; }
        public string RoomType { get; set; } = "Unknown";
        public string Building { get; set; } = "Unknown";
        public Dictionary<string, int> RoomEquipmentDict { get; set; } = new();
        public SpecialRoomModelForOurFEDev() { }
        public SpecialRoomModelForOurFEDev(Room room)
        {
            GenericMapper.MapObjects(this, room);
            RoomEquipmentDict = room.RoomEquipment.GroupBy(re => re.Name).Select(re => new
            {
                re.Key,
                Count = re.Count()
            }).ToDictionary(re => re.Key, re => re.Count);
        }
        public Room GetRoom()
        {
            Room room = new Room();

            GenericMapper.MapObjects(room, this);

            foreach (var item in RoomEquipmentDict)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    room.RoomEquipment.Add(new RoomEquipment
                    {
                        Name = item.Key,
                        EquipmentRef = $"ER_{Guid.NewGuid()}"
                    });
                }
            }

            return room;
        }
    }
}
