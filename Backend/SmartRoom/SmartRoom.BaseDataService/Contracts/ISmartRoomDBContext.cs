using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.BaseDataService.Persistence
{
    public interface ISmartRoomDBContext
    {
        DbSet<Room>? Rooms { get; set; }
        DbSet<RoomEquipment>? RoomEquipments { get; set; }
    }
}
