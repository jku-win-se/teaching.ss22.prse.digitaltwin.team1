using Microsoft.EntityFrameworkCore;
using SmartRoom.BaseDataService.Persistence.Contracts;
using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.BaseDataService.Persistence
{
    public class SmartRoomDBContext : DbContext, ISmartRoomDBContext
    {
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<RoomEquipment>? RoomEquipments { get; set; }
        public SmartRoomDBContext(DbContextOptions<SmartRoomDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
            .HasIndex(p => new { p.Name }).IsUnique();

            modelBuilder.Entity<RoomEquipment>()
            .HasIndex(p => new { p.Name, p.EquipmentRef }).IsUnique();
        }

    }
}
