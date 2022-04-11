using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.TransDataService.Persistence
{
    public class TransDataDBContext : DbContext
    {
        public DbSet<MeasureState> MeasureStates => Set<MeasureState>();
        public DbSet<BinaryState> BinaryStates => Set<BinaryState>();
        public TransDataDBContext(DbContextOptions<TransDataDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>().HasDiscriminator().IsComplete(false);
        }
    }
}
