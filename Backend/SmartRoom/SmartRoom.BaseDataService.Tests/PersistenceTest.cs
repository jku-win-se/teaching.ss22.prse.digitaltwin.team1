using Microsoft.EntityFrameworkCore;
using SmartRoom.BaseDataService.Persistence;
using Xunit;

namespace SmartRoom.BaseDataService.Tests
{
    public class PersistenceTest
    {
        [Fact]


        private SmartRoomUOW GetInMemoryUOW()
        {
            DbContextOptions<SmartRoomDBContext> options;
            var builder = new DbContextOptionsBuilder<SmartRoomDBContext>();
            builder.UseInMemoryDatabase("TestDB");
            options = builder.Options;
            SmartRoomDBContext context = new (options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return new SmartRoomUOW(context);
        }
    }
}