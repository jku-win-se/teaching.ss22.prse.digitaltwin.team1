using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.BaseDataService.Persistence
{
    public class SmartRoomDBContext : DbContext, ISmartRoomDBContext
    {
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<RoomEquipment>? RoomEquipments { get; set; }
        public DbSet<MeasureState>? MeasureStates { get; set; }
        public DbSet<BinaryState>? BinaryStates { get; set; }
        public SmartRoomDBContext(DbContextOptions<SmartRoomDBContext> options) : base(options)
        {

        }
    }
}
